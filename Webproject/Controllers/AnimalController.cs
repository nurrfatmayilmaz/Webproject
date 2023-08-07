using AnimalStoreMvc.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using AnimalStoreMvc.Models.Domain;
using AnimalStoreMvc.Repositories.Abstract;

namespace AnimalStoreMvc.Controllers
{
    [Authorize]
    public class AnimalController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly IFileService _fileService;
        private readonly IAnimaltypeService _genService;
        public AnimalController(IAnimaltypeService genService, IAnimalService AnimalService, IFileService fileService)
        {
            _animalService = AnimalService;
            _fileService = fileService;
            _genService = genService;
        }
        public IActionResult Add()
        {
            var model = new Animal();
            model.AnimaltypeList = _genService.List().Select(a => new SelectListItem { Text = a.AnimaltypeName, Value = a.Id.ToString() });
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Animal model)
        {
            model.AnimaltypeList = _genService.List().Select(a => new SelectListItem { Text = a.AnimaltypeName, Value = a.Id.ToString() });
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.AnimalImage = imageName;
            }
            var result = _animalService.Add(model);
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
            var model = _animalService.GetById(id);
            var selectedGenres = _animalService.GetAnimaltypeByAnimalId(model.Id);
            MultiSelectList multiAnimaltypeList = new MultiSelectList(_genService.List(), "Id", "AnimalTypeName", selectedGenres);
            model.MultiAnimaltypeList = multiAnimaltypeList;
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Animal model)
        {
            var selectedGenres = _animalService.GetAnimaltypeByAnimalId(model.Id);
            MultiSelectList multiAnimaltypeList = new MultiSelectList(_genService.List(), "Id", "AnimaltypeName", selectedGenres);
            model.MultiAnimaltypeList = multiAnimaltypeList;
            if (!ModelState.IsValid)
                return View(model);
            if (model.ImageFile != null)
            {
                var fileReult = this._fileService.SaveImage(model.ImageFile);
                if (fileReult.Item1 == 0)
                {
                    TempData["msg"] = "File could not saved";
                    return View(model);
                }
                var imageName = fileReult.Item2;
                model.AnimalImage = imageName;
            }
            var result = _animalService.Update(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(AnimalList));
            }
            else
            {
                TempData["msg"] = "Error on server side";
                return View(model);
            }
        }

        public IActionResult AnimalList()
        {
            var data = this._animalService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _animalService.Delete(id);
            return RedirectToAction(nameof(AnimalList));
        }



    }
}