using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LlamasTouristCompanion.Models;
using LlamasTouristCompanion.Interfaces;

namespace LlamasTouristCompanion.Controllers
{
    [Route("api/[controller]")]
    public class BotCacheController : Controller
    {

        private readonly IBotCacheService _botCacheService;
        
        public BotCacheController(IBotCacheService botCacheService)
        {
            _botCacheService = botCacheService;
        }

        [HttpGet]
        public async Task<IEnumerable<BotCache>> Get()
        {
            return await _botCacheService.GetAll();
        }

        [HttpGet("{token}", Name = "GetAnswers")]
        public async Task<List<BotCache>> GetAnswersAsync(BotCacheTokens tokens)
        {
            return await _botCacheService.FilterTokensAsync(tokens.Tokens, tokens.ApartmentId);
        }
    }
}
