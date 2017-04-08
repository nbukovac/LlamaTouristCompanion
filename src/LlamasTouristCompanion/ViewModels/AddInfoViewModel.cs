using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.ViewModels
{
    public class AddInfoViewModel
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public Guid LocationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public AddInfoViewModel(string type, string locationId, string name, string desc)
        {
            Type = type;
            LocationId = Guid.Parse(locationId);
            Name = name;
            Description = desc;
        }

        public AddInfoViewModel()
        {

        }
    }
}
