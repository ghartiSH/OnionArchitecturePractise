using Moq;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Tests.FakeDatas;
using ServiceLayer.PeopleService;
using Microsoft.AspNetCore.Mvc;
using DomainLayer.Models;

namespace Tests.Services
{
    public class PeopleServiceTest
    {
        [Fact]
        public void GetAllTest()
        {
            // Arrange
            var peopleRepository = new Mock<IPeopleRepository>();
            peopleRepository.Setup(_ => _.GetAll()).Returns(FakeData.GetFakePeople());
            var peopleService = new PeopleService(peopleRepository.Object);

            // Act
            var result = peopleService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Any());
        }

        [Fact]
        public void GetByIdTest()
        {
            //Arrange
            var peopleRepository = new Mock<IPeopleRepository>();
            peopleRepository.Setup(p => p.Get(1)).Returns(FakeData.GetFakePeople()[0]);
            var peopleService = new PeopleService(peopleRepository.Object);

            //Act
          
            var result = peopleService.GetById(1);

            //Assert
            Assert.Equal(1, result.PeopleId);

        }

        [Fact]
        public void InsertTest()
        {
            //Arrange
            var peopleRepository = new Mock<IPeopleRepository>();
            var people = new People
            {
                PeopleId = 1,
                Name = "Test Name",
                Email = "test@email.com",
                Address = "Test Address",
                PhoneNumber = "9874563210",
                PaymentType = "Cash"
            };
            peopleRepository.Setup(p => p.Insert(people));
            peopleRepository.Setup(p => p.Get(1)).Returns(people);

            //Act
            var peopleService = new PeopleService(peopleRepository.Object);

            peopleService.Insert(people);
            var result = peopleService.GetById(1);

            //Assert
            Assert.Equal(people, result);

        }

        [Fact]
        public void DeleteTest()
        {
            //Arrange
            var peopleRepository = new Mock<IPeopleRepository>();
            var people = new People
            {
                PeopleId = 1,
                Name = "Test Name",
                Email = "test@email.com",
                Address = "Test Address",
                PhoneNumber = "9874563210",
                PaymentType = "Cash"
            };
            peopleRepository.Setup(p => p.Insert(people));
            peopleRepository.Setup(p => p.Delete(people));
            peopleRepository.Setup(p => p.Get(1)).Returns(new People());
           
            var peopleService = new PeopleService(peopleRepository.Object);

            //Act
            peopleService.Insert(people);

            peopleService.Delete(1);

            var result = peopleService.GetById(1);

            //Assert
            Assert.Null(result.Name);
            

        }

        [Fact]
        public void UpdateTest()
        {
            //Arrange
            var peopleRepository = new Mock<IPeopleRepository>();
            var people = new People
            {
                PeopleId = 1,
                Name = "Updated Name",
                Email = "test@email.com",
                Address = "Test Address",
                PhoneNumber = "9874563210",
                PaymentType = "Cash"
            };
            peopleRepository.Setup(p => p.Get(1)).Returns(people);
            peopleRepository.Setup(p => p.Update(people));

            var peopleService = new PeopleService(peopleRepository.Object);


            //Act
            peopleService.Update(people);
            var result = peopleService.GetById(1);


            //Assert
            Assert.Equal("Updated Name", result.Name);

        }
    }
}
