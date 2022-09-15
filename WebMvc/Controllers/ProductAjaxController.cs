using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ProductService;

namespace WebMvc.Controllers
{
    public class ProductAjaxController : Controller
    {

        private readonly ProductService _productService;
        public ProductAjaxController(ProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();

        }

        public IActionResult GetAll()
        {
            return Ok(_productService.GetAll());
        }


        [HttpPost]
        public IActionResult AddProduct([FromBody] Product product)
        {
            _productService.Insert(product);
            return Ok();
        }

        public IActionResult GetbyID(int id)
        {
            var people = _productService.GetById(id);
            return Ok(people);
        }

        [HttpPost]
        public IActionResult EditProduct([FromBody] Product product)
        {
            _productService.Update(product);
            return Ok();

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return Ok();

        }
    }
}
