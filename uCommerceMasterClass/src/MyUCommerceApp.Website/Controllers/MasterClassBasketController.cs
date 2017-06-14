using System.Web.Mvc;
using MyUCommerceApp.Website.Models;
using UCommerce.EntitiesV2;
using UCommerce.Api;
using UCommerce;

namespace MyUCommerceApp.Website.Controllers
{
	public class MasterClassBasketController : Umbraco.Web.Mvc.RenderMvcController
    {
        [HttpGet]
        public ActionResult Index()
        {
            var basketModel = new PurchaseOrderViewModel();

            PurchaseOrder basket = TransactionLibrary.GetBasket(true) //false - не создавать корзину, если не существует, true всегда создавать корзину, даже если не сущесвует.
                .PurchaseOrder;
            Currency billingCurrency = basket.BillingCurrency;
            basketModel.OrderTotal = new Money(basket.OrderTotal.GetValueOrDefault(),
                billingCurrency).ToString();

            foreach (var  basketOrderLine in basket.OrderLines)
            {
                var orderLineViewModel = new OrderlineViewModel();
                orderLineViewModel.Quantity = basketOrderLine.Quantity;
                orderLineViewModel.ProductName = basketOrderLine.ProductName;
                orderLineViewModel.Sku = basketOrderLine.Sku;
                orderLineViewModel.VariantSku = basketOrderLine.VariantSku;
                orderLineViewModel.Total = new Money(basketOrderLine.Total.GetValueOrDefault(), billingCurrency).ToString();
                orderLineViewModel.OrderLineId = basketOrderLine.OrderLineId;

                basketModel.OrderLines.Add(orderLineViewModel);
            }



            return View("/Views/mc/Basket.cshtml", basketModel);
        }

        [HttpPost]
        public ActionResult Index(PurchaseOrderViewModel model)
        {
            foreach (var orderLineViewModel in model.OrderLines)
            {
                int newQuantity = orderLineViewModel.Quantity;
                int orderLineId = orderLineViewModel.OrderLineId;

                if (model.RemoveOrderlineId == orderLineId)
                    newQuantity = 0;

                TransactionLibrary.UpdateLineItem(orderLineId, newQuantity);
            }
            TransactionLibrary.ExecuteBasketPipeline();

            return Redirect(this.CurrentPage.Url);
        }
    }
}