using DomainLayer.Models;
using DomainLayer.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public interface IProductRepository
    {
        void Delete(Product people);
        Product Get(int id);
        //for web-api with pagination
        List<Product> GetAll(Pagination @params);
        //for webapp without pagination
        List<Product> GetAll();
        void Insert(Product people);
        void Update(Product people);
    }
}
