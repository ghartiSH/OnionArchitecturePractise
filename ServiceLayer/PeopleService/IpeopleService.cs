using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.PeopleService
{
    public interface IPeopleService
    {
        IEnumerable<People> GetAllPeople();
        People GetPeople(int id);
        void InsertPeople(People people);
        void UpdatePeople(People people);
        void DeletePeople(int id);
    }
}
