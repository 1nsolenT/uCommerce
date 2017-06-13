using System.Collections.Generic;
using System.Web.Mvc;
using UCommerce.EntitiesV2;
using MyUCommerceApp.Website.Models;
using UCommerce.Runtime;
using UCommerce.Extensions;
using UCommerce.Api;

namespace MyUCommerceApp.Website.Controllers
{
	public class MasterClassCategoryController : Umbraco.Web.Mvc.RenderMvcController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var categoryViewModel = new CategoryViewModel();

            var catalogCtx = SiteContext.Current.CatalogContext;
            var currentCategory = catalogCtx.CurrentCategory;

            categoryViewModel.Name = currentCategory.DisplayName();
            categoryViewModel.Description = currentCategory.Description();

            categoryViewModel.ImageUrl = Umbraco.TypedMedia(currentCategory.ImageMediaId).Url;

            var productsInCategory = CatalogLibrary.GetProducts(currentCategory);
            categoryViewModel.Products = MapProducts(productsInCategory);

            return View("/views/mc/category.cshtml", categoryViewModel);
        }

        private IList<ProductViewModel> MapProducts(ICollection<Product> productsInCategory)
        {
            IList<ProductViewModel> productViews = new List<ProductViewModel>();

            foreach (var product in productsInCategory)
            {
                var productViewModel = new ProductViewModel();
                productViewModel.Name = product.DisplayName();
                productViewModel.Sku = product.Sku;
                productViewModel.Url = "/product?product=" + product.ProductId;
                productViewModel.PriceCalculation = CatalogLibrary.CalculatePrice(product);
                productViews.Add(productViewModel);
            }

            return productViews;
        }
    }
}