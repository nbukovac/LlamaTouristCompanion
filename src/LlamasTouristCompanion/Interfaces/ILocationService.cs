using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Interfaces
{
    public interface ILocationService
    {
        Task<List<Location>> GetAll();
        Location GetById(string id);
        void Add(AddLocationViewModel location);
        void Update(Location location);
        void Delete(string locationId);

        Task<List<Apartment>> GetNearbyApartments(string locationId, double radius);
        Task<List<Event>> GetNearbyEvents(string locationId, double radius);
        Task<List<Info>> GetNearbyInfo(string locationId, double radius);
    }
}
