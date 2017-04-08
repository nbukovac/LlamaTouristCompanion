using LlamasTouristCompanion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using System.Linq.Expressions;
using LlamasTouristCompanion.ViewModels;

namespace LlamasTouristCompanion.Services
{
    public class OwnerService : IOwnerService
    {

        private readonly IOwnerService _ownerService;

        public OwnerService(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        public void AddOwner(AddOwnerViewModel owner)
        {
            throw new NotImplementedE
        }

        public void DeleteOwner(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Owner>> Filter(Expression<Func<Owner, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Owner GetOwnerById(string id)
        {
            throw new NotImplementedException();
        }

        public Owner GetOwnerByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Owner>> GetOwners()
        {
            throw new NotImplementedException();
        }

        public Task<List<Apartment>> GetOwnersApartments(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateOwner(Owner owner)
        {
            throw new NotImplementedException();
        }
    }
}
