using System.Collections.Generic;
using System.Web.Mvc;
using MyUCommerceApp.Website.Models;
using UCommerce.Api;
using UCommerce.Extensions;

namespace MyUCommerceApp.Website.Controllers
{
	public class MasterClassPartialViewController : Umbraco.Web.Mvc.SurfaceController
    {
        public ActionResult CategoryNavigation()
        {
            var categoryNavigation = new CategoryNavigationViewModel();
            categoryNavigation.Categories = MapCategories(CatalogLibrary.GetRootCategories());

            return View("/views/mc/PartialViews/CategoryNavigation.cshtml", categoryNavigation);
        }

        private IList<CategoryViewModel> MapCategories(ICollection<UCommerce.EntitiesV2.Category> categoriesToMap)
        {
            var categoriesToReturn = new List<CategoryViewModel>();
            foreach (var category in categoriesToMap)
            {
                var categoryViewModel = new CategoryViewModel();

                //categoryViewModel.Name = category.Name;
                categoryViewModel.Name = category.DisplayName();

                categoryViewModel.Url = CatalogLibrary.GetNiceUrlForCategory(category);
                categoryViewModel.Url = "/category?category=" + category.CategoryId;

                categoryViewModel.Categories = MapCategories(category.Categories);
                //categoryViewModel.Categories = MapCategories(CatalogLibrary.GetCategories(category));
                categoriesToReturn.Add(categoryViewModel);
            }

            return categoriesToReturn;
        }
    }
}