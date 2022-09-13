using DomainLayer.Models;
using DomainLayer.ModelViews;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PeopleService;

namespace OnionArch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : Controller
    {
        private readonly PeopleService _peopleService;

        public PeopleController(PeopleService peopleService)
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


        [HttpGet(nameof(GetAll))]
        public IActionResult GetAll([FromQuery] Pagination @params)
        {
            var result = _peopleService.GetAll(@params);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records");

        }

        [HttpPost(nameof(Insert))]
        public IActionResult Insert(People people)
        {
            _peopleService.Insert(people);
            return Ok("New People Added");

        }

        [HttpPut(nameof(Update))]
        public IActionResult Update(People people)
        {
            _peopleService.Update(people);
            return Ok("People Updated");

        }

        [HttpDelete(nameof(Delete))]
        public IActionResult Delete(int Id)
        {
            _peopleService.Delete(Id);
            return Ok("People Deleted");

        }
    }
}
