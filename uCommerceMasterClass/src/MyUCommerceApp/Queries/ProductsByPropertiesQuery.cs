using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using UCommerce.EntitiesV2.Queries;
using UCommerce.EntitiesV2;
using NHibernate.Linq;

namespace MyUCommerceApp.BusinessLogic.Queries
{
    public class ProductsByPropertiesQuery : ICannedQuery<Product>
    {
        //public void Execute(ISession session)
        //{
        //    
        //}
        string _fieldName;
        string _propertyName;
        public ProductsByPropertiesQuery(string fieldName, string propertyValue)
        {
            this._fieldName = fieldName;
            this._propertyName = propertyValue;
        }

        public IEnumerable<Product> Execute(ISession session)
        {
            return session.Query<Product>()
                .Where(i =>
                    i.ProductProperties.Any(y =>
                    y.ProductDefinitionField.Name == _fieldName && y.Value == _propertyName));
        }

        //IEnumerable<T> ICannedQuery<T>.Execute(ISession session)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
