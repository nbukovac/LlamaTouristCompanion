using LlamasTouristCompanion.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LlamasTouristCompanion.Models;
using System.Linq.Expressions;
using LlamasTouristCompanion.Repositories;

namespace LlamasTouristCompanion.Services
{
    public class BotCacheService : IBotCacheService
    {

        private readonly IRepository<BotCache, Guid> _botCacheRepository;
        private readonly IApartmentService _apartmentService;

        public BotCacheService(IRepository<BotCache, Guid> botCacheRepository, IApartmentService apartmentService)
        {
            _botCacheRepository = botCacheRepository;
            _apartmentService = apartmentService;
        }

        public void Add(BotCache botCache)
        {
            _botCacheRepository.Insert(botCache);
        }

        public void Delete(string id)
        {
            _botCacheRepository.Delete(Guid.Parse(id));
        }

        public Task<List<BotCache>> Filter(Expression<Func<BotCache, bool>> predicate)
        {
            return _botCacheRepository.GetAllWhere(predicate);
        }

        public async Task<List<BotCache>> FilterTokensAsync(List<string> tokens, string apartmentId)
        {
            var botCacheList = new List<BotCache>();

            foreach (var token in tokens)
            {
                var botCache = await Filter(
                    m => m.ApartmentId.ToString() == apartmentId && token.ToUpper() == m.Keyword.ToUpper());

                var temp = botCache.FirstOrDefault();


                if (temp != null)
                {
                    botCacheList.Add(temp);
                }
            }

            return botCacheList;
        }

        public Task<List<BotCache>> GetAll()
        {
            return _botCacheRepository.GetAll();
        }

        public BotCache GetbyId(string id)
        {
            return _botCacheRepository.GetById(Guid.Parse(id));
        }

        public void Update(BotCache botCache)
        {
            _botCacheRepository.Update(botCache);
        }
    }
}
