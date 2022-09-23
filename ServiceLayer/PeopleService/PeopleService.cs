using DomainLayer.Models;
using DomainLayer.ModelViews;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.PeopleService
{
    public class PeopleService : IPeopleService
    {
        private IPeopleRepository _repository;
        
        public PeopleService(IPeopleRepository repository)
        {
            _repository = repository;
        }

        //for web-api with pagination
        public List<People> GetAll(Pagination @params)
        {
            return _repository.GetAll(@params);
        }

        //for webapp without pagination
        public List<People> GetAll()
        {
            return _repository.GetAll();
        }

        public People GetById(int id)
        {
            return _repository.Get(id);
        }

        public void Insert(People people)
        {
            _repository.Insert(people);
        }
        public void Update(People people)
        {
            _repository.Update(people);
        }

        public void Delete(int id)
        {
            People people = GetById(id);
            _repository.Delete(people);
        }
    }
}
