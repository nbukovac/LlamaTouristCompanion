using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Interfaces;
using LlamasTouristCompanion.ViewModels;

namespace LlamasTouristCompanion.Controllers
{
    public class EventController : Controller
    {

        private readonly IEventService _eventService;
        private readonly ILocationService _locationService;

        public EventController(IEventService eventService, ILocationService locationService)
        {
            _eventService = eventService;
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _eventService.GetAll());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Locations = await _locationService.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AddEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                _eventService.Add(model);
                return RedirectToAction("Index");
            }

            ViewBag.Locations = await _locationService.GetAll();
            return View(model);
        }

    }
}
