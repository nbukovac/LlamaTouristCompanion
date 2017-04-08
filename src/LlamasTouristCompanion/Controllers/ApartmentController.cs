using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Interfaces;
using Microsoft.AspNetCore.Identity;
using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.ViewModels;

namespace LlamasTouristCompanion.Controllers
{
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
                return RedirectToAction("Index");
            }

            return View(model);
        }

        private async Task<Guid> GetActiveUserId()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Guid.Parse(user.Id);
        }
    }
}
