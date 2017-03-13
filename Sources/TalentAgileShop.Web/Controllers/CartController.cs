using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Microsoft.ApplicationInsights;
using TalentAgileShop.Model;

namespace TalentAgileShop.Web.Controllers
{
    [System.Web.Http.RoutePrefix("_api/cart")]
    public class CartController : ApiController
    {
        private readonly ICartRepository _cartRepository;
        private readonly IDataContext _dataContext;
        private readonly ICartPriceCalculator _priceCalculator;

        public CartController(ICartRepository cartRepository, IDataContext dataContext, ICartPriceCalculator priceCalculator)
        {
            _cartRepository = cartRepository;
            _dataContext = dataContext;
            _priceCalculator = priceCalculator;
        }

        private string GetCookieId()
        {
            var cookie =
                Request.Headers.GetCookies().SelectMany(c => c.Cookies).FirstOrDefault(c => c.Name == "cart-id");
           

            if (cookie == null)
            {
                return Guid.NewGuid().ToString();
            }
            var value = cookie.Value;

            return value;
        }

        [System.Web.Http.Route("")]
        public HttpResponseMessage Get()
        {
            var id = GetCookieId();


            var cart = _cartRepository.Get(id);

            if (cart == null)
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
            else
            {
                return Cart(id, cart);
            }

        }



        [System.Web.Http.Route("")]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage CartAction([FromBody] CartAction action)
        {
            if ((action == null) || (action.ProductId == null) || (action.Command == null))
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            var telemetry = new TelemetryClient();
            telemetry.TrackEvent("Cart", new Dictionary<string, string>
            {
                ["command"] = action.Command
            });

            var id = GetCookieId();

            Model.Cart cart;
            switch (action.Command)
            {
                case "add":
                    cart = _cartRepository.AddProduct(id, action.ProductId);
                    break;
                case "remove":
                    cart = _cartRepository.RemoveProduct(id, action.ProductId);
                    break;
                case "delete":
                    cart = _cartRepository.DeleteProduct(id, action.ProductId);
                    break;
                default:
                    return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            return Cart(id, cart);
        }


        private HttpResponseMessage Cart(string id, Model.Cart cart)
        {

            var response = Request.CreateResponse<Model.Cart>(
        HttpStatusCode.OK,
        cart
    );

            return response;

        }
        [System.Web.Http.Route("price")]
        [HttpGet]
        public IHttpActionResult GetCartPrice([FromUri]string discountCode = null)
        {
           
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var id = GetCookieId();
            if (string.IsNullOrWhiteSpace(discountCode))
            {
                discountCode = null;
            }

            var cart = _cartRepository.Get(id);


            var products = _dataContext.GetCartProducts(cart);

            var price = _priceCalculator.ComputePrice(products, discountCode);


            stopwatch.Stop();
            var telemetry = new TelemetryClient();
            telemetry.TrackEvent("CartPrice", new Dictionary<string, string>
            {
                ["code"] = discountCode,
            }, new Dictionary<string,double>()
            {
                ["Duration"] = stopwatch.ElapsedMilliseconds
            });


            if (price.InvalidDiscountCode)
            {
               
                telemetry.TrackEvent("InvalidDiscountCode", new Dictionary<string, string>
                {
                    ["DiscountCode"] = discountCode
                });
            }
            return Ok(price);
        }
       
    }




    public class CartAction
    {
        public string Command { get; set; }

        public string ProductId { get; set; }
    }
}
