using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class BotCacheTokens
    {

        public List<string> Tokens { get; set; }
        public string ApartmentId { get; set; }

        public BotCacheTokens(List<string> tokens, string apartmentId)
        {
            Tokens = tokens;
            ApartmentId = apartmentId;
        }

        public BotCacheTokens()
        {

        }
    }
}
