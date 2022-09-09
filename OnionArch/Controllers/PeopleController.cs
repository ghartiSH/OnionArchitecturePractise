using DomainLayer.Models;
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

        [HttpGet(nameof(GetPeople))]
        public IActionResult GetPeople(int id)
        {
            var result = _peopleService.GetPeople(id);
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");

        }
        [HttpGet(nameof(GetAllPeople))]
        public IActionResult GetAllPeople()
        {
            var result = _peopleService.GetAllPeople();
            if (result is not null)
            {
                return Ok(result);
            }
            return BadRequest("No records found");

        }
        [HttpPost(nameof(InsertPeople))]
        public IActionResult InsertPeople(People people)
        {
            _peopleService.InsertPeople(people);
            return Ok("Data inserted");

        }
        [HttpPut(nameof(UpdatePeople))]
        public IActionResult UpdatePeople(People people)
        {
            _peopleService.UpdatePeople(people);
            return Ok("Updae done");

        }
        [HttpDelete(nameof(DeletePeople))]
        public IActionResult DeletePeople(int Id)
        {
            _peopleService.DeletePeople(Id);
            return Ok("Data Deleted");

        }
    }
}
