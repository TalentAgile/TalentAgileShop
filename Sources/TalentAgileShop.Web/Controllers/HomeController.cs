using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.ApplicationInsights;
using TalentAgileShop.Model;
using TalentAgileShop.Web.Infrastructure;
using TalentAgileShop.Web.Models;

namespace TalentAgileShop.Web.Controllers
{
    [System.Web.Mvc.RoutePrefix("")]
    [CartCookieActionFilter]
    public class HomeController : Controller
    {
        private readonly FeatureSet _featureSet;
        private readonly IDataContext _dataContext;
        private readonly ICartRepository _cartRepository;
        private readonly ICartPriceCalculator _cartPriceCalculator;

        public HomeController(FeatureSet featureSet, IDataContext dataContext, ICartRepository cartRepository, ICartPriceCalculator cartPriceCalculator)
        {
            _featureSet = featureSet;
            _dataContext = dataContext;
            _cartRepository = cartRepository;
            _cartPriceCalculator = cartPriceCalculator;
        }


        [System.Web.Mvc.Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.Route("catalog")]
        public ActionResult Catalog([FromUri]string view,[FromUri]string category)
        {
            if (!_featureSet.CatalogCategories)
            {
                category = null;
            }


            var query = _dataContext.Products.Include(p=> p.Image).Include(p => p.Category);
            if (category != null)
            {
                query = query.Where(p => p.Category.Name == category);
            }
            
                
                
            var products = query.Include(p => p.Origin).OrderBy(p => p.Name).ToList();
            var categories = _dataContext.Categories.OrderBy(c => c.Name).Select(c => c.Name).ToList();

            var viewModel = new CatalogViewModel(products, categories)
            {
                ThumbnailViewAvailable = _featureSet.ThumbnailView,
                ShowCategories = _featureSet.CatalogCategories,
                CurrentCategory = category,
            };
            if (_featureSet.ThumbnailView && view == "thumbnail")
            {
                viewModel.CurrentViewType = CatalogViewModel.ViewType.Thumbnail;
            }
            else
            {
                viewModel.CurrentViewType = CatalogViewModel.ViewType.List;
            }
        
            return View(viewModel);
            
        }

        [System.Web.Mvc.Route("products/{id}")]
        public ActionResult Product(string id)
        {
            var telemetry = new TelemetryClient();

            var product =
               _dataContext.Products.Include(p => p.Category).Include(p => p.Origin).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {                
                telemetry.TrackEvent("ProductNotFound", new Dictionary<string, string>
                {
                    ["id"] = id,
                });
                return HttpNotFound();
            }

            var viewModel = new ProductViewModel(product);




            telemetry.TrackEvent("ShowProduct", new Dictionary<string, string>
            {
                ["Size"] = product.Size.ToString(),
                ["Origin"] = product.Origin.Name,
                ["Category"] = product.Category.Name
            });

            return View(viewModel);
        }


        [System.Web.Mvc.Route("products/{id}/image")]
        public ActionResult ProductImage(string id)
        {
            var product =
                _dataContext.Products.Include(p => p.Image).FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return this.File(product.Image.Data, "image/png");
        }


        [System.Web.Mvc.Route("cart")]
        public ActionResult Cart(string discountCode)
        {
            var basket = GetBasket();

            var products = _dataContext.GetCartProducts(basket);

            var price = _cartPriceCalculator.ComputePrice(products, discountCode);


            return View(new CartViewModel(products, price) { DiscountCode = discountCode});
        }

        [System.Web.Mvc.Route("about")]
        public ActionResult About()
        {
            throw new NotImplementedException();
        }

        private Model.Cart GetBasket()
        {
            var cookie = Request.Cookies.Get("cart-id");


            if (cookie == null)
            {
                return new Model.Cart();
            }

            var id = cookie.Value;

            if (id == null)
            {
                return new Model.Cart();
            }

            return _cartRepository.Get(id);
        }
    }
}