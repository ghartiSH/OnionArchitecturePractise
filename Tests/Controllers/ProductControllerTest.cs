using DomainLayer.Models;
using FluentAssert;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnionArch.Controllers;
using ServiceLayer.ProductService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tests.FakeDatas;
using Xunit;

namespace Tests.Controllers
{
    public class ProductControllerTest
    {
        [Fact]
        public void GetAllPeopleTest()
        {
            //Arrange
            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetAll()).Returns(FakeData.GetFakeProducts());
            var productController = new ProductController(productService.Object);

            //Act
            var result = (OkObjectResult)productController.GetAll();

            //Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);

        }

        [Fact]
        public void GetByIdTest()
        {
            //Arrange
            var productService = new Mock<IProductService>();
            productService.Setup(p => p.GetById(1)).Returns(FakeData.GetFakeProducts()[0]);
            var productController = new ProductController(productService.Object);

            //Act
            var result = (OkObjectResult)productController.GetById(1);
            var people = FakeData.GetFakePeople()[0];

            //Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);
            Assert.Equal(1, people.PeopleId);

        }

        [Fact]
        public void InsertTest()
        {
            //Arrange
            var productService = new Mock<IProductService>();
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 200,
                PeopleId = 1,
            };
            productService.Setup(p => p.Insert(product));
            productService.Setup(p => p.GetById(1)).Returns(product);

            var productController = new ProductController(productService.Object);

            //Act
            var result = (OkObjectResult)productController.Insert(product);
            var result2 = (OkObjectResult)productController.GetById(1);

            //Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);
            result2.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);

        }

        [Fact]
        public void DeleteTest()
        {
            //Arrange
            var productService = new Mock<IProductService>();
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 200,
                PeopleId = 1,
            };

            productService.Setup(p => p.Delete(1));

            var productController = new ProductController(productService.Object);

            //Act
            var result = (OkObjectResult)productController.Delete(1);

            //Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);

        }

        [Fact]
        public void UpdateTest()
        {
            //Arrange
            var productService = new Mock<IProductService>();
            var product = new Product
            {
                ProductId = 1,
                ProductName = "Test",
                Price = 200,
                PeopleId = 1,
            };
            productService.Setup(p => p.Update(product));

            var productController = new ProductController(productService.Object);

            //Act
            var result = (OkObjectResult)productController.Update(product);

            //Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);

        }
    }
}
