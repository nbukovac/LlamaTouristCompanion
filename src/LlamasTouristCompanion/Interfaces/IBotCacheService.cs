using LlamasTouristCompanion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Interfaces
{
    public interface IBotCacheService
    {
        Task<List<BotCache>> GetAll();
        BotCache GetbyId(string id);
        void Add(BotCache botCache);
        void Update(BotCache botCache);
        void Delete(string id);

        Task<List<BotCache>> Filter(Expression<Func<BotCache, bool>> predicate);
        Task<List<BotCache>> FilterTokensAsync(List<string> tokens, string apartmentId);
    }
}
