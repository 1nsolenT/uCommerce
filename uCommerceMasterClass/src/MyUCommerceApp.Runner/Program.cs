using System;
using System.Linq;
using MyUCommerceApp.BusinessLogic.Queries;
using NHibernate.Linq;
using UCommerce.EntitiesV2;
using UCommerce.Infrastructure;
using UCommerce.Search.RavenDB;
using NHibernate;

namespace MyUCommerceApp.Integration
{
	class Program
	{
		static void Main(string[] args)
		{
            var firstOrder = PurchaseOrder
                .All()
                .First();
            
            var repo = ObjectFactory.Instance.Resolve<IRepository<PurchaseOrder>>();
            firstOrder = repo
                .Select()  // incapsulate provider.GetSession().Query<PurchaseOrder>();
                .First(i => i.Shipments.Any());
            
            ISessionProvider provider = ObjectFactory.Instance.Resolve<ISessionProvider>();
            ISession session = provider.GetSession();
            firstOrder = session
                .Query<PurchaseOrder>()
                .First();

            var ordersByCetainFromStaticLayer = PurchaseOrder.All().Where(i =>
                i.CompletedDate > new DateTime(2009, 1, 1)).ToList();





            var productRepository = ObjectFactory.Instance.Resolve<IRepository<Product>>();
            var productOnHomePage = productRepository.Select(i => i.ProductProperties.Any(
                y => y.ProductDefinitionField.Name == "ShowOnHomepage" &&
                y.Value == "True"
                )).ToList();

            productOnHomePage = productRepository.Select(
                new ProductsByPropertiesQuery("ShowOnHomePage", "True")).ToList();





            var provider_1 = ObjectFactory.Instance.Resolve<ISessionProvider>();
            using (var session_1 = provider_1.GetSession())
            {
                var orders = session_1.Query<PurchaseOrder>().Fetch(i=>i.Customer).ToList();
                foreach (var order in orders)
                {
                    if (order.Customer != null)
                    {
                        Console.WriteLine(order.Customer.FirstName);
                    }
                }
            }
            Console.ReadLine();


            var provide_2 = ObjectFactory.Instance.Resolve<ISessionProvider>();
            provide_2.GetSession().Query<Product>().Where(i => i.ProductId == 105)
                .FetchMany(x => x.Variants) //4
                .FetchMany(x => x.CategoryProductRelations) //4
                .FetchMany(x => x.ProductRelations) //4
                .ToList(); // total 4*4*4 be aware
            


        }
	}
}
