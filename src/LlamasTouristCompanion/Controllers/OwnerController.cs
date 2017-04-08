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

        public OwnerController(IOwnerService ownerService, UserManager<ApplicationUser> userManager)
        {
            _ownerService = ownerService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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

        private async Task<Guid> GetActiveUserId()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Guid.Parse(user.Id);
        }
    }
}
