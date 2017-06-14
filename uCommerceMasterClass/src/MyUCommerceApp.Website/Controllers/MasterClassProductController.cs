using System.Collections.Generic;
using System.Web.Mvc;
using UCommerce.EntitiesV2;
using MyUCommerceApp.Website.Models;
using UCommerce.Runtime;
using UCommerce.Extensions;
using UCommerce.Api;

namespace MyUCommerceApp.Website.Controllers
{
	public class MasterClassProductController : Umbraco.Web.Mvc.RenderMvcController
    {
        [HttpGet]
        public ActionResult Index()
		{
			ProductViewModel productModel = new ProductViewModel();

            productModel = MapProduct(SiteContext.Current.CatalogContext.CurrentProduct);

            return View("/views/mc/product.cshtml", productModel);
		}

        private ProductViewModel MapProduct(Product currentProduct)
        {
            var model = new ProductViewModel();
            model.Sku = currentProduct.Sku;
            model.Name = currentProduct.DisplayName();
            model.LongDescription = currentProduct.LongDescription();
            model.PriceCalculation = CatalogLibrary.CalculatePrice(currentProduct);

            model.VariantSku = currentProduct.VariantSku;
            model.IsVariant = currentProduct.IsVariant;

            foreach (var currentProductVariant in currentProduct.Variants)
            {
                model.Variants.Add(MapProduct(currentProductVariant)); 
            }
            return model;

        }

		private IList<ProductViewModel> MapVariants(ICollection<Product> variants)
		{
			var variantModels = new List<ProductViewModel>();

			return variantModels;
		}

		[HttpPost]
		public ActionResult Index(AddToBasketViewModel model)
		{
            //PurchaseOrder basket = new PurchaseOrder();
            //uCommerce_PurchaseOrder table

            //TransactionLibrary.GetBasket();
            //TransactionLibrary.ExecuteBasketPipeline(); ;

            TransactionLibrary.AddToBasket(1, model.Sku, model.VariantSku);


            return Index();
        }
    }
}