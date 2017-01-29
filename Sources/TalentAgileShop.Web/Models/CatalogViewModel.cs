using System.Collections.Generic;
using System.Web.Mvc;
using TalentAgileShop.Model;

namespace TalentAgileShop.Web.Models
{
    public class CatalogViewModel
    {
        public List<string> Categories { get; }

        public List<Product> Products { get; }

        public ViewType CurrentViewType { get; set; }

        public bool ThumbnailViewAvailable { get; set; }

        public bool ShowCategories { get; set; }
        public string CurrentCategory { get; set; }

        public CatalogViewModel(List<Product> products, List<string> categories)
        {
            Products = products;
            Categories = categories;
        }


        public enum ViewType
        {
            List,
            Thumbnail
        }
    }
}