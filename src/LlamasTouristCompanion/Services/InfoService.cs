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
    public class InfoService : IInfoService
    {

        private readonly IRepository<Info, Guid> _infoRepository;

        public InfoService(IRepository<Info, Guid> infoRepository)
        {
            _infoRepository = infoRepository;
        }

        public void Add(AddInfoViewModel info)
        {
            _infoRepository
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Info>> Filter(Expression<Func<Info, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<List<Info>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Info GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Info info)
        {
            throw new NotImplementedException();
        }
    }
}
