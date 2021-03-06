﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Interfaces;
using LlamasTouristCompanion.Models;
using Microsoft.AspNetCore.Identity;
using LlamasTouristCompanion.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace LlamasTouristCompanion.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Index()
        {
            var user = await GetActiveUserId();
            Owner owner = await _ownerService.GetOwnerByUserIdAsync(user.ToString());
            List<Apartment> apartments = await _ownerService.GetOwnersApartments(owner.OwnerId.ToString());
            ViewBag.Apartments = apartments;
            return View(owner);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddOwnerViewModel model)
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
