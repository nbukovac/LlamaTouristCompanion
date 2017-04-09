using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class BotCacheLocationTokens
    {

        public List<string> Tokens { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public BotCacheLocationTokens(List<string> tokens, double longitude, double latitude)
        {
            Tokens = tokens;
            Longitude = longitude;
            Latitude = latitude;
        }

        public BotCacheLocationTokens()
        {

        }
    }
}
