using LlamasTouristCompanion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using System.Linq.Expressions;
using LlamasTouristCompanion.Repositories;
using LlamasTouristCompanion.ViewModels;

namespace LlamasTouristCompanion.Services
{
    public class ApartmentService : IApartmentService
    {

        private readonly IRepository<Apartment, Guid> _apartmentRepository;
        private readonly IOwnerService _ownerService;

        public ApartmentService(IRepository<Apartment, Guid> apartmentRepository, IOwnerService ownerService)
        {
            _apartmentRepository = apartmentRepository;
            _ownerService = ownerService;
        }

        public async Task AddAsync(AddApartmentViewModel apartment, string userId)
        {
            var owner = await _ownerService.GetOwnerByUserIdAsync(userId);
            _apartmentRepository.Insert(new Apartment(apartment, owner.OwnerId));
        }

        public void Delete(string id)
        {
            _apartmentRepository.Delete(Guid.Parse(id));
        }

        public Task<List<Apartment>> Filter(Expression<Func<Apartment, bool>> predicate)
        {
            return _apartmentRepository.GetAllWhere(predicate);
        }

        public Task<List<Apartment>> GetAll()
        {
            return _apartmentRepository.GetAll();
        }

        public Apartment GetById(string id)
        {
            return _apartmentRepository.GetById(Guid.Parse(id));
        }

        public Location GetLocation(string apartmentId)
        {
            return GetById(apartmentId).Location;
        }

        public Owner GetOwner(string apartmentId)
        {
            return GetById(apartmentId).Owner;
        }

        public void Update(Apartment apartment)
        {
            _apartmentRepository.Update(apartment);
        }
    }
}
