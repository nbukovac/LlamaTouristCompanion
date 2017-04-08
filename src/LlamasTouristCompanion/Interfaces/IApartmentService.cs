using LlamasTouristCompanion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Interfaces
{
    public interface IApartmentService
    {
        Task<List<Apartment>> GetAll();
        Apartment GetById(string id);
        void Add(Apartment apartment);
        void Update(Apartment apartment);
        void Delete(string id);

        Owner GetOwner(string apartmentId);
        Task<List<Apartment>> Filter(Expression<Func<Apartment, bool>> predicate);
        Location GetLocation(string apartmentId);
    }
}
