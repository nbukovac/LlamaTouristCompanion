using LlamasTouristCompanion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Interfaces
{
    public interface IOwnerService
    {
        Task<List<Owner>> GetOwners();
        Owner GetOwnerById(string id);
        void AddOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(string id);

        Owner GetOwnerByUserId(string userId);
        Task<List<Apartment>> GetOwnersApartments(string id);
        Task<List<Owner>> Filter(Expression<Func<Owner, bool>> predicate);
    }
}
