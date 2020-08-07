
using DevExpress.Xpo;
using XpoDemoOrm;
using System;
using BIT.Xpo;
using BIT.Xpo.Providers.WebApi.Client;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Register XpoWebApiProvider 
            XpoWebApiProvider.Register();

           
          
            var ConnectionString = XpoWebApiProvider.GetConnectionString("https://localhost:44389", "/XpoWebApi", string.Empty, "001");

            XpoInitializer xpoInitializer = new XpoInitializer(ConnectionString, typeof(Invoice), typeof(Customer));
            xpoInitializer.InitSchema();
          
            using (var UoW = xpoInitializer.CreateUnitOfWork())
            {
                var faker = new Bogus.Faker<Customer>().CustomInstantiator(c => new Customer(UoW))
                            .RuleFor(p => p.Code, f => f.Random.Guid())
                            .RuleFor(p => p.Name, f => f.Name.FullName())
                            .RuleFor(p => p.Active, p => p.Random.Bool());

                var Customers = faker.Generate(100);
                if (UoW.InTransaction)
                    UoW.CommitChanges();
            }
            ;
        }
    }
}
