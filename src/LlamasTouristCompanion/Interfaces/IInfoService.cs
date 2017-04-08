using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Interfaces
{
    public interface IInfoService
    {
        Task<List<Info>> GetAll();
        Info GetById(string id);
        void Add(AddInfoViewModel info);
        void Update(Info info);
        void Delete(string id);

        Task<List<Info>> Filter(Expression<Func<Info, bool>> predicate);
    }
}
