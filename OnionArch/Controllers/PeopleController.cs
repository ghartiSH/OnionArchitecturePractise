using DomainLayer.Models;
using DomainLayer.ModelViews;
using DomainLayer.Validators;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PeopleService;

namespace OnionArch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : Controller
    {
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            _peopleService = peopleService; 
        }

        [HttpGet (nameof(GetById))]
        public IActionResult GetById(int id)
        {
            var result = _peopleService.GetById(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("People not found");

        }


        /*[HttpGet(nameof(GetAll))]
        public IActionResult GetAll([FromQuery] Pagination @params)
        {
            var result = _peopleService.GetAll(@params);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records");

        }*/

        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll()
        {
            var result = _peopleService.GetAll();
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records");

        }

        [HttpPost(nameof(Insert))]
        public IActionResult Insert(People people)
        {
            var validator = new PeopleValidator();

            var valid = validator.Validate(people);

            if (valid.IsValid)
            {
                _peopleService.Insert(people);
                return Ok("New People Added");
            }
            /*if (ModelState.IsValid)
            {
                _peopleService.Insert(people);
                return Ok("New People Added");
            }*/
            return BadRequest(valid.Errors.Select(s => s.ErrorMessage).ToList());
        }

        [HttpPut(nameof(Update))]
        public IActionResult Update(People people)
        {
            var validator = new PeopleValidator();

            var valid = validator.Validate(people);

            if (valid.IsValid)
            {
                _peopleService.Update(people);
                return Ok("People updated");
            }

            /*if (ModelState.IsValid)
            {
                _peopleService.Update(people);
                return Ok("People Updated");
            }*/

            return BadRequest(valid.Errors.Select(s => s.ErrorMessage).ToList());

        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            _peopleService.Delete(Id);
            return Ok("People Deleted");

        }
    }
}
