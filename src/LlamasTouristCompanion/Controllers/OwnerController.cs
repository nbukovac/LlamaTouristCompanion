using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Interfaces;
using LlamasTouristCompanion.Models;
using Microsoft.AspNetCore.Identity;
using LlamasTouristCompanion.ViewModels;

namespace LlamasTouristCompanion.Controllers
{
    public class OwnerController : Controller
    {

        private readonly IOwnerService _ownerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApartmentService _apartmentService;

        public OwnerController(IOwnerService ownerService, UserManager<ApplicationUser> userManager,
            IApartmentService apartmentService)
        {
            _ownerService = ownerService;
            _userManager = userManager;
            _apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string ownerId)
        {
            return View(await _ownerService.GetOwnersApartments(ownerId));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(AddOwnerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _ownerService.AddOwner(model, await GetActiveUserId());
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            return View(_ownerService.GetOwnerById(id));
        }

        [HttpPost]
        public IActionResult Edit(Owner owner)
        {
            if (ModelState.IsValid)
            {
                _ownerService.UpdateOwner(owner);
                return RedirectToAction("Index");
            }

            return View(owner);
        }

        private async Task<Guid> GetActiveUserId()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Guid.Parse(user.Id);
        }
    }
}
