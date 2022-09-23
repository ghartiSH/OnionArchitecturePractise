using Bogus;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Tests.FakeDatas
{
    public static class FakeData
    {
        public static List<People>? FakePeople;

        public static List<People> GetFakePeople()
        {
            
            var phone = new string[] { "9874561230", "9874563320", "9856230147", "9874120365" };
            var payment = new string[] { "Cash", "Card", "Online" };
            int id = 1;
            FakePeople = (new Faker<People>()
                .RuleFor(p => p.PeopleId, f => id++)
                .RuleFor(p => p.Name, f => f.Person.FullName)
                .RuleFor(p => p.Email, f => f.Person.Email)
                .RuleFor(p => p.Address, f => f.Address.City())
                .RuleFor(p => p.PhoneNumber, f => f.PickRandom(phone))
                .RuleFor(p => p.PaymentType, f => f.PickRandom(payment))
                ).Generate(20);

            return FakePeople;
        }


        public static  List<Product> GetFakeProducts()
        {
            var price = new int[] { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
            var fakePeople = GetFakePeople();

            var fakeProducts = new List<Product>();

            var fakeProduct = new List< Product>();
            
            int id = 1;
            foreach (var people in fakePeople)
            {
                

                fakeProduct = (new Faker<Product>()
                .RuleFor(p => p.ProductId, p => id++)
                .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => f.PickRandom(price))
                .RuleFor(p => p.PeopleId, f => people.PeopleId)
                ).GenerateBetween(1, 2);

                fakeProducts.AddRange(fakeProduct);
            }

            return fakeProducts;
        }
    }
}
