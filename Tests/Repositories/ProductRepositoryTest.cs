using DomainLayer.Models;
using DomainLayer.ModelViews;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.FakeDatas;
using Xunit;

namespace Tests.Repositories
{
    public class ProductRepositoryTest
    {
        protected readonly ApplicationDbContext _context;
        public ProductRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureCreated();
        }


        [Fact]
        public void GetAllTest()
        {
            /// Arrange     

            var fakeProducts = FakeData.GetFakeProducts();
            _context.products.AddRange(fakeProducts);
            _context.SaveChanges();

            var productRepository = new ProductRepository(_context);

            /// Act
            var result = productRepository.GetAll();

            /// Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Any());
        }



        [Fact]
        public void InsertTest()
        {
            //Arrange
            var productRepository = new ProductRepository(_context);

            var product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 200,
                PeopleId = 1,

            };

            //Act
            productRepository.Insert(product);

            var check = productRepository.Get(product.ProductId);

            //Assert
            Assert.NotNull(check);
            Assert.Equal(product.ProductId, check.ProductId);
            Assert.Equal(product.ProductName, check.ProductName);

        }

        [Fact]
        public void GetByIdTest()
        {
            //Arrange
            var productRepository = new ProductRepository(_context);
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 200,
                PeopleId = 1,
            };

            //Act
            productRepository.Insert(product);

            var check = productRepository.Get(1);

            //Assert
            Assert.NotNull(check);
            Assert.Equal(1, check.ProductId);
            Assert.Equal(product.ProductName, check.ProductName);
        }

        [Fact]
        public void DeleteTest()
        {
            //Arrange
            var productRepository = new ProductRepository(_context);
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 200,
                PeopleId = 1,
            };

            //Act
            productRepository.Insert(product);

            productRepository.Delete(product);

            var check = productRepository.Get(1);
            var count = productRepository.GetAll().Count();

            //Assert
            Assert.Null(check);
            Assert.True(count == 0);

        }

        [Fact]
        public void PaginationTest()
        {
            //Arrange
            var fakeProduct = FakeData.GetFakeProducts();
            _context.products.AddRange(fakeProduct);
            _context.SaveChanges();

            var productRepository = new ProductRepository(_context);

            var page = new Pagination
            {
                Page = 1,
                ItemsPerPage = 8
            };

            /// Act
            var result = productRepository.GetAll(page).Count();

            //Assert
            Assert.Equal(8, result);

        }

        [Fact]
        public void UpdateTest()
        {
            //Arrange
            var fakeProduct = FakeData.GetFakeProducts();
            _context.products.AddRange(fakeProduct);
            _context.SaveChanges();

            var productRepository = new ProductRepository(_context);

            //Act
            var check = productRepository.Get(1);

            check.ProductName = "Updated Name";

            productRepository.Update(check);

            var result = productRepository.Get(1);

            //Assert
            Assert.Equal(1, check.ProductId);
            Assert.Equal("Updated Name", result.ProductName);


        }
    }
}
