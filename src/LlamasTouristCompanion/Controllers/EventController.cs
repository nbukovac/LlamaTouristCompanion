using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Interfaces;
using LlamasTouristCompanion.ViewModels;
using LlamasTouristCompanion.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            ViewBag.Locations = new SelectList(await _locationService.GetAll(), "LocationId", "Address");
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

            ViewBag.Locations = new SelectList(await _locationService.GetAll(), "LocationId", "Address");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Locations = new SelectList(await _locationService.GetAll(), "LocationId", "Address");
            return View(_eventService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(Event model)
        {
            if (ModelState.IsValid)
            {
                _eventService.Update(model);
                return RedirectToAction("Index");
            }

            ViewBag.Locations = new SelectList(await _locationService.GetAll(), "LocationId", "Address");
            return View(model);
        }

        public IActionResult Delete(string id)
        {
            _eventService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
