using DomainLayer.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.PeopleService;
using WebMvc.Models;

namespace WebMvc.Controllers
{
    public class PeopleController : Controller
    {
        private readonly IPeopleService _peopleService;
        public PeopleController(IPeopleService peopleService)
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
                    Address = p.Address,
                    PhoneNumber = p.PhoneNumber,
                    PaymentType = p.PaymentType
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
            if (ModelState.IsValid)
            {
                _peopleService.Insert(people);
                return RedirectToAction("Index");

            }
            return View(people);
        }

        public IActionResult EditPeople(int id)
        {
            var people = _peopleService.GetById(id);

           /* PeopleViewModel peopleViewModel = new PeopleViewModel
            {
                PeopleId = people.PeopleId,
                Name = people.Name,
                Email = people.Email,
                Address = people.Address,
                PhoneNumber = people.PhoneNumber,
                PaymentType = people.PaymentType
            };*/
            return View(people);
        }

        [HttpPost]
        public IActionResult EditPeople(People people)
        {
            if (ModelState.IsValid)
            {
                _peopleService.Update(people);
                return RedirectToAction("Index");

            }
            return View(people);
        }

        public IActionResult DeletePeople(int id)
        {
            var people = _peopleService.GetById(id);


            PeopleViewModel peopleViewModel = new PeopleViewModel
            {
                PeopleId = people.PeopleId,
                Name = people.Name,
                Email = people.Email,
                Address = people.Address,
                PhoneNumber = people.PhoneNumber,
                PaymentType = people.PaymentType
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
