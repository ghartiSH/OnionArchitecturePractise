using DomainLayer.Models;
using DomainLayer.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.PeopleService
{
    public interface IPeopleService
    {
        List<People> GetAll(Pagination p);
        List<People> GetAll();
        People GetById(int id);
        void Insert(People people);
        void Update(People people);
        void Delete(int id);
    }
}
