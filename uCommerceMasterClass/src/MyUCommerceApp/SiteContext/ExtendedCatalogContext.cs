using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using UCommerce.Content;
using UCommerce.EntitiesV2;
using UCommerce.Runtime;

namespace MyUCommerceApp.BusinessLogic.SiteContext
{
    class ExtendedCatalogContext : CatalogContext
    {
        public ExtendedCatalogContext(IDomainService domainService,
            IRepository<ProductCatalogGroup> productCatalogGroupRepository,
            IRepository<ProductCatalog> productCatalogRepository,
            IRepository<PriceGroup> priceGroupRepository) 
            : base(domainService, productCatalogGroupRepository, productCatalogRepository, priceGroupRepository)
        {

        }

        public override string CurrentCatalogName
        {
            get
            {
                if (UserIsLogginIn())
                    return "Private";
                return base.CurrentCatalogName;
            }

            set
            {
                base.CurrentCatalogName = value;
            }
        }

        private bool UserIsLogginIn()
        {
            //need to be in UMBRAOC development environment to imlement this logic
            return HttpContext.Current.Request.QueryString["IsLoggedIn"] != null;
            
        }
    }
}
