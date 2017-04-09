using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Interfaces;
using Microsoft.AspNetCore.Identity;
using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LlamasTouristCompanion.Controllers
{
    [Authorize]
    public class ApartmentController : Controller
    {

        private readonly IApartmentService _apartmentService;
        private readonly IOwnerService _ownerService;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApartmentController(IApartmentService apartmentService, IOwnerService ownerService,
            UserManager<ApplicationUser> userManager)
        {
            _apartmentService = apartmentService;
            _ownerService = ownerService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
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

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return View(_apartmentService.GetById(id));
        }

        [HttpPost]
        public IActionResult Edit(Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _apartmentService.Update(apartment);
                return RedirectToAction("Index", "Owner");
            }

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
