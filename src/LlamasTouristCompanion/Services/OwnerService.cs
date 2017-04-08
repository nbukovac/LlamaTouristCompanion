using LlamasTouristCompanion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using System.Linq.Expressions;
using LlamasTouristCompanion.ViewModels;
using LlamasTouristCompanion.Repositories;

namespace LlamasTouristCompanion.Services
{
    public class OwnerService : IOwnerService
    {

        private readonly IRepository<Owner, Guid> _ownerRepository;
        private readonly IRepository<Apartment, Guid> _apartmentRepository;

        public OwnerService(IRepository<Owner, Guid> ownerRepository, IRepository<Apartment,
            Guid> apartmentRepository)
        {
            _ownerRepository = ownerRepository;
            _apartmentRepository = apartmentRepository;
        }

        public void AddOwner(AddOwnerViewModel owner, Guid userId)
        {
            _ownerRepository.Insert(new Owner(owner, userId));
        }

        public void DeleteOwner(string id)
        {
            _ownerRepository.Delete(Guid.Parse(id));
        }

        public Task<List<Owner>> Filter(Expression<Func<Owner, bool>> predicate)
        {
            return _ownerRepository.GetAllWhere(predicate);
        }

        public Owner GetOwnerById(string id)
        {
            return _ownerRepository.GetById(Guid.Parse(id));
        }

        public async Task<Owner> GetOwnerByUserIdAsync(string userId)
        {
            var owners = await _ownerRepository.GetAllWhere(m => m.UserId.ToString() == userId);
            return owners.FirstOrDefault();
        }

        public Task<List<Owner>> GetOwners()
        {
            return _ownerRepository.GetAll();
        }

        public Task<List<Apartment>> GetOwnersApartments(string id)
        {
            return _apartmentRepository.GetAllWhere(m => m.OwnerId.ToString() == id);
        }

        public void UpdateOwner(Owner owner)
        {
            _ownerRepository.Update(owner);
        }
    }
}
