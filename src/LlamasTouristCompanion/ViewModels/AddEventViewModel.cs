using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.ViewModels
{
    public class AddEventViewModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid LocationId { get; set; }
        [Required]
        public string Name { get; set; }


        public AddEventViewModel()
        {

        }

        public AddEventViewModel(DateTime date, string desc, string locationId, string name)
        {
            Date = date;
            Description = desc;
            LocationId = Guid.Parse(locationId);
            Name = name;
        }
    }
}
