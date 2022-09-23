using DomainLayer.Models;
using DomainLayer.ModelViews;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class PeopleRepository: IPeopleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        

        public PeopleRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }
        public void Delete(People people)
        {
            if (people == null)
            {
                throw new ArgumentNullException("People not found");
            }
            _applicationDbContext.Remove(people);
            _applicationDbContext.SaveChanges();
        }


        public People Get(int id)
        {
            var people = _applicationDbContext.people.SingleOrDefault(p => p.PeopleId == id);
            if (people == null)
            {
                return null;
            }
            return people; 
        }


        //for web-api with pagination
        public List<People> GetAll(Pagination @params)
        {
            var peopleData = _applicationDbContext.people
                .OrderBy(p => p.PeopleId)
                .Skip((@params.Page - 1) * @params.ItemsPerPage)
                .Take(@params.ItemsPerPage)
                .ToList();

            return peopleData;
        }

        //for webapp without pagination
        public List<People> GetAll()
        {
            var peopleData = _applicationDbContext.people.ToList();
            return peopleData;
        }

        public void Insert(People people)
        {
            if (people == null)
            {
                throw new ArgumentNullException("Model invalid");
            }
            _applicationDbContext.people.Add(people);
            _applicationDbContext.SaveChanges();
        }



        public void Update(People people)
        {
            if (people == null)
            {
                throw new ArgumentNullException("Model Invalid");
            }
            _applicationDbContext.Update(people);
            _applicationDbContext.SaveChanges();
        }
    }
}
