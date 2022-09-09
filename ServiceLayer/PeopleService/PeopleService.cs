using DomainLayer.Models;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.PeopleService
{
    public class PeopleService: IPeopleService
    {
        private IPeopleRepository<People> _repository;
        
        public PeopleService(IPeopleRepository<People> repository)
        {
            _repository = repository;
        }

        public IEnumerable<People> GetAllPeople()
        {
            return _repository.GetAll();
        }

        public People GetPeople(int id)
        {
            return _repository.Get(id);
        }

        public void InsertPeople(People people)
        {
            _repository.Insert(people);
        }
        public void UpdatePeople(People people)
        {
            _repository.Update(people);
        }

        public void DeletePeople(int id)
        {
            People people = GetPeople(id);
            _repository.Remove(people);
            _repository.SaveChanges();
        }
    }
}
