using DomainLayer.Models;
using Moq;
using RepositoryLayer.Repositories;
using ServiceLayer.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.FakeDatas;
using Xunit;

namespace Tests.Services
{
    public class ProductServiceTest
    {
        [Fact]
        public void GetAllTest()
        {
            // Arrange
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(_ => _.GetAll()).Returns(FakeData.GetFakeProducts());
            var productService = new ProductService(productRepository.Object);

            // Act
            var result = productService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Any());
        }

        [Fact]
        public void GetByIdTest()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(p => p.Get(1)).Returns(FakeData.GetFakeProducts()[0]);
            var productService = new ProductService(productRepository.Object);

            //Act

            var result = productService.GetById(1);

            //Assert
            Assert.Equal(1, result.ProductId);

        }

        [Fact]
        public void InsertTest()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 200,
                PeopleId = 1,
            };
            productRepository.Setup(p => p.Insert(product));
            productRepository.Setup(p => p.Get(1)).Returns(product);

            //Act
            var productService = new ProductService(productRepository.Object);

            productService.Insert(product);
            var result = productService.GetById(1);

            //Assert
            Assert.Equal(product, result);

        }

        [Fact]
        public void DeleteTest()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 200,
                PeopleId = 1,
            };
            productRepository.Setup(p => p.Insert(product));
            productRepository.Setup(p => p.Delete(product));
            productRepository.Setup(p => p.Get(1)).Returns(new Product());

            var productService = new ProductService(productRepository.Object);

            //Act
            productService.Insert(product);

            productService.Delete(1);

            var result = productService.GetById(1);

            //Assert
            Assert.Null(result.ProductName);

        }

        [Fact]
        public void UpdateTest()
        {
            //Arrange
            var productRepository = new Mock<IProductRepository>();
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Updated Name",
                Price = 200,
                PeopleId = 1,
            };
            productRepository.Setup(p => p.Get(1)).Returns(product);
            productRepository.Setup(p => p.Update(product));

            var productService = new ProductService(productRepository.Object);


            //Act
            productService.Update(product);
            var result = productService.GetById(1);


            //Assert
            Assert.Equal("Updated Name", result.ProductName);

        }
    }
}
