using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.Interfaces;
using System;

namespace LlamasTouristCompanion.Controllers
{
    [Route("api/[controller]")]
    public class BotCacheController : Controller
    {

        private readonly IBotCacheService _botCacheService;
        private readonly ILocationService _locationService;
        private readonly IApartmentService _apartmentService;
        private const double DegreeToKm = 111.12;
        private const double Radius = 10;

        public BotCacheController(IBotCacheService botCacheService, ILocationService locationService,
            IApartmentService apartmentService)
        {
            _botCacheService = botCacheService;
            _locationService = locationService;
            _apartmentService = apartmentService;
        }

        [HttpGet]
        public async Task<IEnumerable<BotCache>> Get()
        {
            return await _botCacheService.GetAll();
        }

        [HttpGet("{tokens}", Name = "GetAnswers")]
        public async Task<List<BotCache>> GetAnswers(BotCacheTokens tokens)
        {
            return await _botCacheService.FilterTokensAsync(tokens.Tokens, tokens.ApartmentId);
        }

        [HttpGet("{location}", Name = "GetApartments")]
        public async Task<List<Apartment>> GetLocationApartmentsAsync(LocationApi location)
        {
            return await GetApartmentsAsync(location);
        }

        [HttpGet(Name = "FillMe")]
        public IActionResult FillMe()
        {
            return Ok();
        }


        private async Task<List<Apartment>> GetApartmentsAsync(LocationApi location)
        {
            List<Apartment> apartments = await _apartmentService.GetAll();
            List<Apartment> nearby = new List<Apartment>();

            foreach (var apartment in apartments)
            {
                var apartmentLocation = _locationService.GetById(apartment.LocationId.ToString());
                var inRadius = Math.Pow((location.Latitude - apartmentLocation.Latitude) * DegreeToKm, 2)
                    + Math.Pow((location.Longitude - apartmentLocation.Latitude) * DegreeToKm, 2) < Math.Pow(Radius, 2);

                if (inRadius)
                {
                    nearby.Add(apartment);
                }
            }

            return nearby;
        }
    }
}
