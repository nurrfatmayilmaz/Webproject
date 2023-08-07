using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AnimalStoreMvc.Models.Domain;
using AnimalStoreMvc.Repositories.Abstract;
using AnimalStoreMvc.Repositories.Abstract;
using AnimalStoreMvc.Repositories.Implementation;

namespace AnimalStoreMvc.Controllers
{
    //[Authorize]
    public class AnimaltypeController : Controller
    {
        private readonly IAnimaltypeService _animaltypeService;
        public AnimaltypeController(IAnimaltypeService animaltypeService)
        {
            _animaltypeService = animaltypeService;
        }
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Animaltype model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _animaltypeService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
            var data = _animaltypeService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(Animaltype model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var result = _animaltypeService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(AnimaltypeList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult AnimaltypeList()
        {
            var data = this._animaltypeService.List().ToList();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _animaltypeService.Delete(id);
            return RedirectToAction(nameof(AnimaltypeList));
        }



    }
}