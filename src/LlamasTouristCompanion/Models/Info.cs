using LlamasTouristCompanion.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class Info
    {
        public Guid InfoId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public Guid LocationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual Location Location { get; set; }

        public Info(string type, Guid locationId, string name, string description)
        {
            InfoId = Guid.NewGuid();
            Type = type;
            LocationId = locationId;
            Name = name;
            Description = description;
        }

        public Info()
        {

        }

        public Info(AddInfoViewModel model)
        {
            InfoId = Guid.NewGuid();
            Type = model.Type;
            LocationId = model.LocationId;
            Name = model.Name;
            Description = model.Description;
        }
    }
}
