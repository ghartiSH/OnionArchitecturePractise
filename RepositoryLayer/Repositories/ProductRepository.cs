using DomainLayer.Models;
using DomainLayer.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class ProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;


        public ProductRepository(ApplicationDbContext context)
        {
            _applicationDbContext = context;
        }
        public void Delete(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("People not found");
            }
            _applicationDbContext.Remove(product);
            _applicationDbContext.SaveChanges();
        }


        public Product Get(int id)
        {
            var product = _applicationDbContext.products.SingleOrDefault(p => p.ProductId == id);
            return product;

        }



        public List<Product> GetAll(Pagination @params)
        {
            var productData = _applicationDbContext.products
                .OrderBy(p => p.ProductId)
                .Skip((@params.Page - 1) * @params.ItemsPerPage)
                .Take(@params.ItemsPerPage)
                .ToList();

            return productData;
        }

        public List<Product> GetAll()
        {
            var productData = _applicationDbContext.products.ToList();

            return productData;
        }

        public void Insert(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Model invalid");
            }
            _applicationDbContext.products.Add(product);
            _applicationDbContext.SaveChanges();
        }

        public void Update(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException("Model Invalid");
            }
            _applicationDbContext.Update(product);
            _applicationDbContext.SaveChanges();
        }
    }
}
