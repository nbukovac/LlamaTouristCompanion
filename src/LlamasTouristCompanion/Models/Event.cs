using LlamasTouristCompanion.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class Event
    {
        public Guid EventId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid LocationId { get; set; }

        public virtual Location Location { get; set; }


        public Event(DateTime date, string description, Guid locationId)
        {
            EventId = Guid.NewGuid();
            Date = date;
            Description = description;
            LocationId = locationId;
        }

        public Event()
        {
            
        }

        public Event(AddEventViewModel model)
        {
            EventId = Guid.NewGuid();
            Date = model.Date;
            Description = model.Description;
            LocationId = model.LocationId;
        }
    }
}
