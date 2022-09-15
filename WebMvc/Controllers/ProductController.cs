using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ProductService;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        public IActionResult Index()
        {
            List<ProductViewModel> product = new List<ProductViewModel>();
            var data = _productService.GetAll().ToList();

            data.ForEach(p =>
            {
                ProductViewModel prodViewModel = new ProductViewModel
                {
                    Id = p.ProductId,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    PeopleId = p.PeopleId,
                };
                product.Add(prodViewModel);
            }
            );
            return View(product);

        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            _productService.Insert(product);
            return RedirectToAction("Index");
        }

        public IActionResult EditProduct(int id)
        {
            var product = _productService.GetById(id);

            ProductViewModel prodViewModel = new ProductViewModel
            {
                Id = product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                PeopleId = product.PeopleId,
            };
            return View(prodViewModel);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            _productService.Update(product);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteProduct(int id)
        {
            var product = _productService.GetById(id);

            ProductViewModel prodViewModel = new ProductViewModel
            {
                Id =product.ProductId,
                ProductName = product.ProductName,
                Price = product.Price,
                PeopleId=product.PeopleId,
            };
            return View(prodViewModel);
        }

        [HttpPost]
        public IActionResult DeleteProduct(Product product)
        {
            _productService.Delete(product.ProductId);
            return RedirectToAction("Index");
        }
    }
}
