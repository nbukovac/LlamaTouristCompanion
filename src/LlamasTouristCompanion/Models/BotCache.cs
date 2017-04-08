using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class BotCache
    {
        public Guid BotCacheId { get; set; }
        [Required]
        public string Keyword { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public Guid ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }


        public BotCache(string keyword, string answer, Guid apartmentId)
        {
            EventId = Guid.NewGuid();
            Keyword = keyword;
            Answer = answer;
            ApartmentId = apartmentId;
        }

        public Event()
        {
            
        }
    }
}
