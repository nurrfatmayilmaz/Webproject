using Microsoft.AspNetCore.Mvc;
using AnimalStoreMvc.Repositories.Abstract;
using AnimalStoreMvc.Repositories.Implementation;

namespace AnimalStoreMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnimalService _animalService;
        public HomeController(IAnimalService animalService)
        {
            _animalService = animalService;
        }
        public IActionResult Index(string term = "", int currentPage = 1)
        {
            var animals = _animalService.List(term, true, currentPage);
            return View(animals);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult AnimalDetail(int animalId)
        {
            var animal = _animalService.GetById(animalId);
            return View(animal);
        }

    }
}
