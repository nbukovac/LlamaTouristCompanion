﻿using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.ViewModels;
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
        void AddOwner(AddOwnerViewModel owner, Guid userId);
        void UpdateOwner(Owner owner);
        void DeleteOwner(string id);

        Task<Owner> GetOwnerByUserIdAsync(string userId);
        Task<List<Apartment>> GetOwnersApartments(string id);
        Task<List<Owner>> Filter(Expression<Func<Owner, bool>> predicate);
    }
}
