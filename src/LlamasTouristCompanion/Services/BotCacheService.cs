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
        private readonly IInfoService _infoService;
        private readonly IEventService _eventService;

        public BotCacheService(IRepository<BotCache, Guid> botCacheRepository, 
            IApartmentService apartmentService, IInfoService infoService, IEventService eventService)
        {
            _botCacheRepository = botCacheRepository;
            _apartmentService = apartmentService;
            _infoService = infoService;
            _eventService = eventService;
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

        public async Task FillBotAsync()
        {
            BotCache botCache = null;
            List<BotCache> inCache = null;

            var apartments = await _apartmentService.GetAll();

            //var infos = await _infoService.GetAll();
            //var events = await _eventService.GetAll();

            foreach (var apartment in apartments)
            {
                var split = apartment.Utilities.Split('&');

                foreach (var utility in split)
                {
                    var utilitySplit = utility.Split('=');
                    botCache = new BotCache(utilitySplit[0], utilitySplit[1],
                        apartment.ApartmentId);

                    inCache = await (_botCacheRepository.GetAllWhere(
                        m => m.ApartmentId == apartment.ApartmentId 
                        && m.Keyword == utilitySplit[0]));

                    if (!inCache.Any())
                    {
                        _botCacheRepository.Insert(botCache);
                    }
                }

                var inTime = apartment.CheckIn.TimeOfDay.ToString();
                var outTime = apartment.CheckOut.TimeOfDay.ToString();

                botCache = new BotCache("check", inTime + "&" + outTime, apartment.ApartmentId);

                inCache = await _botCacheRepository.GetAllWhere(m => m.ApartmentId == apartment.ApartmentId
                    && m.Keyword == "check");

                if (!inCache.Any())
                {
                    _botCacheRepository.Insert(botCache);
                }
            }
        }
    }
}
