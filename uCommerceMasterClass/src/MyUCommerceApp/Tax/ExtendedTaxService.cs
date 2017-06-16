using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UCommerce;
using UCommerce.Catalog;
using UCommerce.EntitiesV2;

namespace MyUCommerceApp.BusinessLogic.Tax
{
    public class ExtendedTaxService : TaxService
    {
        //public Money CalculateTax(PriceGroup priceGroup, Money amount)
        //{
        //    throw new NotImplementedException();
        //}
        //
        //public Money CalculateTax(Product product, PriceGroup priceGroup, Money unitPrice)
        //{
        //    throw new NotImplementedException();
        //}
        private IRepository<PriceGroup> _priceGroupRepository;
        public ExtendedTaxService(IRepository<PriceGroup> priceGroupRepository)
        {
            _priceGroupRepository = priceGroupRepository;
        }
        public override Money CalculateTax(Product product, PriceGroup priceGroup, Money unitPrice)
        {
            var priceGroup_1 = product.GetProperty("PriceGroupCategory").GetValue();
            if (priceGroup_1 != null)
            {
                int id;
                if (int.TryParse(priceGroup_1.ToString(), out id))
                {
                    priceGroup = _priceGroupRepository.Get(id);
                }
            }



            var priceGroupProperty = product["PriceGroupCategory"].GetValue()?.ToString();
            if (priceGroupProperty == null)
            {
                priceGroupProperty = product.ParentProduct["PriceGroupCategory"].GetType()?.ToString();
            }
            if (priceGroupProperty == null && product.ParentProduct == null)
                return base.CalculateTax(product, priceGroup, unitPrice);

            var customPriceGroup = _priceGroupRepository.SingleOrDefault(i =>     //PriceGroup.Get()&
                i.PriceGroupId == int.Parse(priceGroupProperty));
            return base.CalculateTax(product, customPriceGroup, unitPrice);
        }

    }
}
