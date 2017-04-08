using LlamasTouristCompanion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.Repositories;
using LlamasTouristCompanion.ViewModels;

namespace LlamasTouristCompanion.Services
{
    public class LocationService : ILocationService
    {
        private const double DegreeToKm = 111.12;


        private readonly IRepository<Location, Guid> _locationRepository;
        private readonly IRepository<Apartment, Guid> _apartmentRepository;
        private readonly IRepository<Event, Guid> _eventsRepository;
        private readonly IRepository<Info, Guid> _infoRepository;

        public LocationService(IRepository<Location, Guid> locationRepository, 
            IRepository<Apartment, Guid> apartmentRepository, 
            IRepository<Event, Guid> eventsRepository,
            IRepository<Info, Guid> infoRepository)
        {
            _locationRepository = locationRepository;
            _apartmentRepository = apartmentRepository;
            _eventsRepository = eventsRepository;
            _infoRepository = infoRepository;
        }

        public void Add(AddLocationViewModel location)
        {
            _locationRepository.Insert(new Location(location));
        }

        public void Delete(string locationId)
        {
            _locationRepository.Delete(Guid.Parse(locationId));
        }

        public Task<List<Location>> GetAll()
        {
            return _locationRepository.GetAll();
        }

        public Location GetById(string id)
        {
            return _locationRepository.GetById(Guid.Parse(id));
        }

        public async Task<List<Apartment>> GetNearbyApartments(string locationId, double radius)
        {
            var location = GetById(locationId);
            List<Apartment> apartments = await _apartmentRepository.GetAll();
            List<Apartment> nearby = new List<Apartment>();

            foreach (var apartment in apartments)
            {
                var apartmentLocation = _locationRepository.GetById(apartment.LocationId);
                var inRadius = Math.Pow((location.Latitude - apartmentLocation.Latitude) * DegreeToKm, 2)
                    + Math.Pow((location.Longitude - apartmentLocation.Latitude) * DegreeToKm, 2) < Math.Pow(radius, 2);

                if (inRadius)
                {
                    nearby.Add(apartment);
                }
            }

            return nearby;
        }

        public async Task<List<Event>> GetNearbyEvents(string locationId, double radius)
        {
            var location = GetById(locationId);
            List<Event> events = await _eventsRepository.GetAll();
            List<Event> nearby = new List<Event>();

            foreach (var e in events)
            {
                var eventLocation = _locationRepository.GetById(e.LocationId);
                var inRadius = Math.Pow((location.Latitude - eventLocation.Latitude) * DegreeToKm, 2)
                    + Math.Pow((location.Longitude - eventLocation.Latitude) * DegreeToKm, 2) < Math.Pow(radius, 2);

                if (inRadius)
                {
                    nearby.Add(e);
                }
            }

            return nearby;
        }

        public async Task<List<Info>> GetNearbyInfoAsync(string locationId, double radius)
        {
            var location = GetById(locationId);
            List<Info> infos = await _infoRepository.GetAll();
            List<Info> nearby = new List<Info>();

            foreach (var info in infos)
            {
                var eventLocation = _locationRepository.GetById(info.LocationId);
                var inRadius = Math.Pow((location.Latitude - eventLocation.Latitude) * DegreeToKm, 2)
                    + Math.Pow((location.Longitude - eventLocation.Latitude) * DegreeToKm, 2) < Math.Pow(radius, 2);

                if (inRadius)
                {
                    nearby.Add(info);
                }
            }

            return nearby;
        }

        public void Update(Location location)
        {
            _locationRepository.Update(location);
        }
    }
}
