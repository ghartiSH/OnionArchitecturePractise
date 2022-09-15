using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PeopleService;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class PeopleAjaxController : Controller
    {
        private readonly PeopleService _peopleService;
        public PeopleAjaxController(PeopleService peopleService)
        {
            _peopleService = peopleService; 
        }
        
        public IActionResult Index()
        {
           return View();   

        }

        public IActionResult GetAll()
        {
            return Ok(_peopleService.GetAll());
        }

       
        [HttpPost]
        public IActionResult AddPeople([FromBody] People people)
        {
            _peopleService.Insert(people);
            return Ok();
        }

        public IActionResult GetbyID(int id)
        {
            var people = _peopleService.GetById(id);
            return Ok(people);
        }

        [HttpPost]
        public IActionResult EditPeople([FromBody] People people)
        {
            _peopleService.Update(people);
            return Ok();

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _peopleService.Delete(id);
            return Ok();

        }

    }

}
