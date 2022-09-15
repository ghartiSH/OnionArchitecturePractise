using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PeopleService;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly PeopleService _peopleService;
        public PeopleController(PeopleService peopleService)
        {
            _peopleService = peopleService;
        }
        public IActionResult Index()
        {
            List<PeopleViewModel> people = new List<PeopleViewModel>();
            var data = _peopleService.GetAll().ToList();

            data.ForEach(p =>
            {
                PeopleViewModel peopleViewModel = new PeopleViewModel
                {
                    PeopleId = p.PeopleId,
                    Name = p.Name,
                    Email = p.Email,
                    Address = p.Address
                };
                people.Add(peopleViewModel);
            }
            );
            return View(people);

        }

        public IActionResult AddPeople()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPeople(People people)
        {
            _peopleService.Insert(people);
            return RedirectToAction("Index");   
        }

        public IActionResult EditPeople(int id)
        {
            var people = _peopleService.GetById(id);

            PeopleViewModel peopleViewModel = new PeopleViewModel
            {
                PeopleId = people.PeopleId,
                Name = people.Name,
                Email = people.Email,
                Address = people.Address
            };
            return View(peopleViewModel);
        }

        [HttpPost]
        public IActionResult EditPeople(People people)
        {
            _peopleService.Update(people);
            return RedirectToAction("Index");
        }

        public IActionResult DeletePeople(int id)
        {
            var people = _peopleService.GetById(id);


            PeopleViewModel peopleViewModel = new PeopleViewModel
            {
                PeopleId = people.PeopleId,
                Name = people.Name,
                Email = people.Email,
                Address = people.Address
            };
            return View(peopleViewModel);
        }

        [HttpPost]
        public IActionResult DeletePeople(People people)
        {
            _peopleService.Delete(people.PeopleId);
            return RedirectToAction("Index");
        }

    }
}
