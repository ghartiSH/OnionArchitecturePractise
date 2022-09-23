using DomainLayer.Models;
using DomainLayer.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public interface IPeopleRepository
    {
        void Delete(People people);
        People Get(int id);
        //for web-api with pagination
        List<People> GetAll(Pagination @params);
        //for webapp without pagination
        List<People> GetAll();
        void Insert(People people);
        void Update(People people);
    }
}
