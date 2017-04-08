using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Interfaces;
using LlamasTouristCompanion.ViewModels;

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
    }
}
