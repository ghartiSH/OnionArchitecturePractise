using Bogus;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public partial class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            /*//alternative for seeding data during migration
            var ids = 1;

            var fakePeople = (new Faker<People>()
                    .RuleFor(p => p.Id, f => ids++)
                    .RuleFor(p => p.Name, f => f.Person.FullName)
                    .RuleFor(p => p.Email, f => f.Person.Email)
                    .RuleFor(p => p.Address, f => f.Address.FullAddress())
                    );

            modelBuilder.Entity<People>().HasData(
                fakePeople.GenerateBetween(10000, 10000)
                );*/
        }

        public DbSet<People> people { get; set; }

        public DbSet<Product> products { get; set; }

        public async Task AddData()
        {
            if (!(await people.AnyAsync()))
            {
                var price = new int[] { 100, 200, 300, 400, 500, 600, 700, 800, 900, 1000 };
                
                var fakePeople = (new Faker<People>()
                    .RuleFor(p => p.Name, f => f.Person.FullName)
                    .RuleFor(p => p.Email, f => f.Person.Email)
                    .RuleFor(p => p.Address, f => f.Address.City())
                    ).Generate(10000);

                foreach(var people in fakePeople)
                {
                    
                    var fakeProduct = (new Faker<Product>()
                        .RuleFor(p => p.ProductName, f => f.Commerce.ProductName())
                        .RuleFor(p => p.Price, f => f.PickRandom(price))
                        ).GenerateBetween(1, 2);

                    people.Products = fakeProduct;
                }

                await people.AddRangeAsync(fakePeople);
                await SaveChangesAsync();
            }
        }

    }

}
