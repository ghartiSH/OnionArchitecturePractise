using Moq;
using ServiceLayer.PeopleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.FakeDatas;
using Xunit;
using OnionArch.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using FluentAssert;
using DomainLayer.Models;

namespace Tests.Controllers
{
    public class PeopleControllerTests
    {
        [Fact]
        public void GetAllPeopleTest()
        {
            //Arrange
            var peopleService = new Mock<IPeopleService>();
            peopleService.Setup(p => p.GetAll()).Returns(FakeData.GetFakePeople());
            var peopleController = new PeopleController(peopleService.Object);

            //Act
            var result = (OkObjectResult)peopleController.GetAll();

            //Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);

        }

        [Fact]
        public void GetByIdTest()
        {
            //Arrange
            var peopleService = new Mock<IPeopleService>();
            peopleService.Setup(p => p.GetById(1)).Returns(FakeData.GetFakePeople()[0]);
            var peopleController = new PeopleController(peopleService.Object);

            //Act
            var result = (OkObjectResult)peopleController.GetById(1);
            var people = FakeData.GetFakePeople()[0];

            //Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);
            Assert.Equal(1, people.PeopleId);
            
        }

        [Fact]
        public void InsertTest()
        {
            //Arrange
            var peopleService = new Mock<IPeopleService>();
            var people = new People
            {
                PeopleId = 1,
                Name = "Test Name",
                Email = "test@email.com",
                Address = "Test Address",
                PhoneNumber = "9874563210",
                PaymentType = "Cash"
            };
            peopleService.Setup(p => p.Insert(people));
            peopleService.Setup(p => p.GetById(1)).Returns(people);

            var peopleController = new PeopleController(peopleService.Object);

            //Act
            var result = (OkObjectResult)peopleController.Insert(people);
            var result2 = (OkObjectResult)peopleController.GetById(1);

            //Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);
            result2.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);

        }

        [Fact]
        public void DeleteTest()
        {
            //Arrange
            var peopleService = new Mock<IPeopleService>();
            var people = new People
            {
                PeopleId = 1,
                Name = "Test Name",
                Email = "test@email.com",
                Address = "Test Address",
                PhoneNumber = "9874563210",
                PaymentType = "Cash"
            };
            
            peopleService.Setup(p => p.Delete(1));
           
            var peopleController = new PeopleController(peopleService.Object);

            //Act
            var result = (OkObjectResult)peopleController.Delete(1);

            //Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);

        }

        [Fact]
        public void UpdateTest()
        {
            //Arrange
            var peopleService = new Mock<IPeopleService>();
            var people = new People
            {
                PeopleId = 1,
                Name = "Test Name",
                Email = "test@email.com",
                Address = "Test Address",
                PhoneNumber = "9874563210",
                PaymentType = "Cash"
            };
            peopleService.Setup(p => p.Update(people));

            var peopleController = new PeopleController(peopleService.Object);

            //Act
            var result = (OkObjectResult)peopleController.Update(people);

            //Assert
            result.StatusCode.ShouldBeEqualTo((int)HttpStatusCode.OK);

        }
    }
}
