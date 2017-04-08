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
            _infoRepository.Insert(new Info(info));
        }

        public void Delete(string id)
        {
            _infoRepository.Delete(Guid.Parse(id));
        }

        public Task<List<Info>> Filter(Expression<Func<Info, bool>> predicate)
        {
            return _infoRepository.GetAllWhere(predicate);
        }

        public Task<List<Info>> GetAll()
        {
            return _infoRepository.GetAll();
        }

        public Info GetById(string id)
        {
            return _infoRepository.GetById(Guid.Parse(id));
        }

        public void Update(Info info)
        {
            _infoRepository.Update(info);
        }
    }
}
