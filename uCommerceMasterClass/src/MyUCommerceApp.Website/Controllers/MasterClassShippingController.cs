using System.Web.Mvc;
using MyUCommerceApp.Website.Models;
using UCommerce.EntitiesV2;
using UCommerce.Api;
using System.Collections.Generic;

namespace MyUCommerceApp.Website.Controllers
{
	public class MasterClassShippingController : Umbraco.Web.Mvc.RenderMvcController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var shippingModel = new ShippingViewModel();

            //OrderAddress shippingInformation = TransactionLibrary.GetShippingInformation("shipment"); // unnesessary
            OrderAddress shippingInformation = TransactionLibrary.GetShippingInformation(); // unnesessary
            ICollection<ShippingMethod> availableShippingMethods = TransactionLibrary
                .GetShippingMethods(/*shippingInformation.Country*/);

            ShippingMethod selectedShippingMethod = TransactionLibrary.GetShippingMethod();
            int selectedShippingMethodId = -1;
            if (selectedShippingMethod != null)
                selectedShippingMethodId = selectedShippingMethod.Id;

            foreach (var method in availableShippingMethods)
            {
                shippingModel.AvailableShippingMethods.Add(new SelectListItem()
                {
                    Selected = method.ShippingMethodId == selectedShippingMethodId,
                    Text = method.Name,
                    Value = method.ShippingMethodId.ToString()
                });
            }


            return View("/Views/mc/Shipping.cshtml", shippingModel);
        }

        [HttpPost]
        public ActionResult Index(ShippingViewModel shipping)
        {
            TransactionLibrary.CreateShipment(shipping.SelectedShippingMethodId,
                 null,
                 true); //create only one shipment method for this bascket
            TransactionLibrary.ExecuteBasketPipeline();
            return Redirect("/payment");
		}
	}
}