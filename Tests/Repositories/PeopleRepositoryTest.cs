using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Tests.FakeDatas;
using RepositoryLayer.Repositories;
using ServiceLayer.PeopleService;
using DomainLayer.Models;
using DomainLayer.ModelViews;

namespace Tests.Repositories
{
    public class PeopleRepositoryTest
    {
        protected readonly ApplicationDbContext _context;
        public PeopleRepositoryTest()
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

            var fakePeople = FakeData.GetFakePeople();
            _context.people.AddRange(fakePeople);
            _context.SaveChanges();

            var peopleRepository = new PeopleRepository(_context);

            /// Act
            var result = peopleRepository.GetAll();

            /// Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.True(result.Any());
        }



        [Fact]
        public void InsertTest()
        {
            //Arrange
            var peopleRepository = new PeopleRepository(_context);

            var people = new People
            {
                PeopleId = 1,
                Name = "Test Name",
                Email = "test@email.com",
                Address = "Test Address",
                PhoneNumber = "9874563210",
                PaymentType =   "Cash"
            };

            //Act
            peopleRepository.Insert(people);

            var check = peopleRepository.Get(people.PeopleId);

            //Assert
            Assert.NotNull(check);
            Assert.Equal(people.PeopleId, check.PeopleId);
            Assert.Equal(people.Name, check.Name);

        }

        [Fact]
        public void GetByIdTest()
        {
            //Arrange
            var peopleRepository = new PeopleRepository(_context);
            var people = new People
            {
                PeopleId = 1,
                Name = "Test Name",
                Email = "test@email.com",
                Address = "Test Address",
                PhoneNumber = "9874563210",
                PaymentType = "Cash"
            };

            //Act
            peopleRepository.Insert(people);

            var check = peopleRepository.Get(1);

            //Assert
            Assert.NotNull(check);
            Assert.Equal(1, check.PeopleId);
            Assert.Equal(people.Name, check.Name);
        }

        [Fact]
        public void DeleteTest()
        {
            //Arrange
            var peopleRepository = new PeopleRepository(_context);
            var people = new People
            {
                PeopleId = 1,
                Name = "Test Name",
                Email = "test@email.com",
                Address = "Test Address",
                PhoneNumber = "9874563210",
                PaymentType = "Cash"
            };

            //Act
            peopleRepository.Insert(people);

            peopleRepository.Delete(people);

            var check = peopleRepository.Get(1);
            var count = peopleRepository.GetAll().Count();

            //Assert
            Assert.Null(check);
            Assert.True(count == 0);
          
        }

        [Fact]
        public void PaginationTest()
        {
            //Arrange
            var fakePeople = FakeData.GetFakePeople();
            _context.people.AddRange(fakePeople);
            _context.SaveChanges();

            var peopleRepository = new PeopleRepository(_context);

            var page = new Pagination
            {
                Page = 1,
                ItemsPerPage = 8
            };

            /// Act
            var result = peopleRepository.GetAll(page).Count();

            //Assert
            Assert.Equal(8, result);

        }

        [Fact]
        public void UpdateTest()
        {
            //Arrange
            var fakePeople = FakeData.GetFakePeople();
            _context.people.AddRange(fakePeople);
            _context.SaveChanges();

            var peopleRepository = new PeopleRepository(_context);

            //Act
            People check = peopleRepository.Get(1);

            check.Name = "Updated Name";

            peopleRepository.Update(check);

            var result = peopleRepository.Get(1);

            //Assert
            Assert.Equal(1, check.PeopleId);
            Assert.Equal("Updated Name", result.Name);

            
        }
    }
}
