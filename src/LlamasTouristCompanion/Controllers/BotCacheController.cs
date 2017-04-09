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
        private readonly IEventService _eventService;
        private readonly IInfoService _infoService;


        private const double DegreeToKm = 111.12;
        private const double Radius = 10;

        public BotCacheController(IBotCacheService botCacheService, ILocationService locationService,
            IApartmentService apartmentService, IEventService eventService, IInfoService infoService)
        {
            _botCacheService = botCacheService;
            _locationService = locationService;
            _apartmentService = apartmentService;
            _eventService = eventService;
            _infoService = infoService;
        }

        [Route("bot")]
        [HttpGet]
        public async Task<IEnumerable<BotCache>> Get()
        {
            return await _botCacheService.GetAll();
        }

        [Route("answers")]
        [HttpGet("{tokens}", Name = "GetAnswers")]
        public async Task<List<BotCache>> GetAnswers(BotCacheTokens tokens)
        {
            return await _botCacheService.FilterTokensAsync(tokens.Tokens, tokens.ApartmentId);
        }

        [Route("apartments")]
        [HttpGet("{location}", Name = "GetApartments")]
        public async Task<List<Apartment>> GetLocationApartmentsAsync(LocationApi location)
        {
            return await GetApartmentsAsync(location);
        }

        [Route("events")]
        [HttpGet("{location}", Name = "GetEvents")]
        public async Task<List<Event>> GetLocationEventsAsync(LocationApi location)
        {
            return await GetEventsAsync(location);
        }

        [Route("info")]
        [HttpGet("{location}", Name = "GetInfos")]
        public async Task<List<Info>> GetLocationInfoAsync(LocationApi location)
        {
            return await GetInfoAsync(location);
        }

        [Route("fill")]
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

        private async Task<List<Event>> GetEventsAsync(LocationApi location)
        {
            List<Event> events = await _eventService.GetAll();
            List<Event> nearby = new List<Event>();

            foreach (var e in events)
            {
                var eventLocation = _locationService.GetById(e.LocationId.ToString());
                var inRadius = Math.Pow((location.Latitude - eventLocation.Latitude) * DegreeToKm, 2)
                    + Math.Pow((location.Longitude - eventLocation.Latitude) * DegreeToKm, 2) < Math.Pow(Radius, 2);

                if (inRadius)
                {
                    nearby.Add(e);
                }
            }

            return nearby;
        }

        private async Task<List<Info>> GetInfoAsync(LocationApi location)
        {
            List<Info> infos = await _infoService.GetAll();
            List<Info> nearby = new List<Info>();

            foreach (var info in infos)
            {
                var eventLocation = _locationService.GetById(info.LocationId.ToString());
                var inRadius = Math.Pow((location.Latitude - eventLocation.Latitude) * DegreeToKm, 2)
                    + Math.Pow((location.Longitude - eventLocation.Latitude) * DegreeToKm, 2) < Math.Pow(Radius, 2);

                if (inRadius)
                {
                    nearby.Add(info);
                }
            }

            return nearby;
        }
    }
}
