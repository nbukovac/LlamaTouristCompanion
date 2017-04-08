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

        public Task<List<Apartment>> GetNearbyApartments(string locationId, double radius)
        {
            return _apartmentRepository.GetAllWhere(m => m.LocationId.ToString() == locationId);
        }

        public Task<List<Event>> GetNearbyEvents(string locationId, double radius)
        {
            return _eventsRepository.GetAllWhere(m => m.LocationId.ToString() == locationId);
        }

        public Task<List<Info>> GetNearbyInfo(string locationId, double radius)
        {
            return _infoRepository.GetAllWhere(m => m.LocationId.ToString() == locationId);
        }

        public void Update(Location location)
        {
            _locationRepository.Update(location);
        }
    }
}
