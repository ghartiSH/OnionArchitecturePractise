using DomainLayer.Models;
using DomainLayer.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProductService
{
    public interface IProductService
    {
        List<Product> GetAll(Pagination p);
        List<Product> GetAll();
        Product GetById(int id);
        void Insert(Product prod);
        void Update(Product prod);
        void Delete(int id);
    }
}
