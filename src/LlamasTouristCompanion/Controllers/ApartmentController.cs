using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Interfaces;
using Microsoft.AspNetCore.Identity;
using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LlamasTouristCompanion.Controllers
{
    [Authorize]
    public class ApartmentController : Controller
    {

        private readonly IApartmentService _apartmentService;
        private readonly IOwnerService _ownerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILocationService _locationService;

        public ApartmentController(IApartmentService apartmentService, IOwnerService ownerService,
            UserManager<ApplicationUser> userManager, ILocationService locationService)
        {
            _apartmentService = apartmentService;
            _ownerService = ownerService;
            _userManager = userManager;
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.LocationId = new SelectList(await _locationService.GetAll(), "LocationId", "Address");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddApartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetActiveUserId();
                var owner = await _ownerService.GetOwnerByUserIdAsync(user.ToString());
                await _apartmentService.AddAsync(model, owner.OwnerId.ToString());

                return RedirectToAction("Index", "Owner");
            }

            ViewBag.LocationId = new SelectList(await _locationService.GetAll(), "LocationId", "Address");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.LocationId = new SelectList(await _locationService.GetAll(), "LocationId", "Address");
            return View(_apartmentService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _apartmentService.Update(apartment);
                return RedirectToAction("Index", "Owner");
            }

            ViewBag.LocationId = new SelectList(await _locationService.GetAll(), "LocationId", "Address");
            return View(apartment);
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            _apartmentService.Delete(id);
            return RedirectToAction("Index", "Owner");
        }

        private async Task<Guid> GetActiveUserId()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Guid.Parse(user.Id);
        }
    }
}
