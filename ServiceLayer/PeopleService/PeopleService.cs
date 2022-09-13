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
    public class PeopleService
    {
        private PeopleRepository _repository;
        
        public PeopleService(PeopleRepository repository)
        {
            _repository = repository;
        }

        public List<People> GetAll(Pagination @params)
        {
            return _repository.GetAll(@params);
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
