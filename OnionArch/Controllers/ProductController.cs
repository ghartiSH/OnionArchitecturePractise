using DomainLayer.Models;
using DomainLayer.ModelViews;
using DomainLayer.Validators;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.ProductService;

namespace OnionArch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet(nameof(GetById))]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("Product not found");

        }


        /*[HttpGet(nameof(GetAll))]
        public IActionResult GetAll([FromQuery] Pagination @params)
        {
            var result = _productService.GetAll(@params);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records");

        }*/

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records");

        }

        [HttpPost(nameof(Insert))]
        public IActionResult Insert(Product product)
        {

            var validator = new ProductValidator();

            var valid = validator.Validate(product);

            if (valid.IsValid)
            {
                _productService.Insert(product);
                return Ok("New Product Added");
            }

            return BadRequest(valid.Errors.Select(s => s.ErrorMessage).ToList());

        }

        [HttpPut(nameof(Update))]
        public IActionResult Update(Product product)
        {
            var validator = new ProductValidator();

            var valid = validator.Validate(product);

            if (valid.IsValid)
            {
                _productService.Update(product);
                return Ok("Product Updated");
            }

            /*_productService.Update(product);
            return Ok("Product Updated");*/

            return BadRequest(valid.Errors.Select(s => s.ErrorMessage).ToList());


        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            _productService.Delete(Id);
            return Ok("Product Deleted");

        }
    }
}
