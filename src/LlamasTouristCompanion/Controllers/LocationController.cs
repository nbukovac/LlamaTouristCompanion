using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Interfaces;
using LlamasTouristCompanion.ViewModels;
using LlamasTouristCompanion.Models;

namespace LlamasTouristCompanion.Controllers
{
    public class LocationController : Controller
    {

        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _locationService.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddLocationViewModel model)
        {
            if (ModelState.IsValid)
            {
                _locationService.Add(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return View(_locationService.GetById(id));
        }
    
        [HttpPost]
        public IActionResult Edit(Location location)
        {
            if (ModelState.IsValid)
            {
                _locationService.Update(location);
                return RedirectToAction("Index");
            }

            return View(location);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            _locationService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
