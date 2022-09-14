using DomainLayer.Models;
using DomainLayer.ModelViews;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ProductService
{
    public class ProductService
    {
        private ProductRepository _repository;

        public ProductService(ProductRepository repository)
        {
            _repository = repository;
        }

        public List<Product> GetAll(Pagination @params)
        {
            return _repository.GetAll(@params);
        }

        public List<Product> GetAll()
        {
            return _repository.GetAll();
        }

        public Product GetById(int id)
        {
            return _repository.Get(id);
        }

        public void Insert(Product product)
        {
            _repository.Insert(product);
        }
        public void Update(Product product)
        {
            _repository.Update(product);
        }

        public void Delete(int id)
        {
            Product product = GetById(id);
            _repository.Delete(product);
        }
    }
}
