using LlamasTouristCompanion.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class Location
    {
        public Guid LocationId { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public Location(double latitude, double longitude)
        {
            LocationId = Guid.NewGuid();
            Latitude = latitude;
            Longitude = longitude;
        }

        public Location()
        {
            
        }

        public Location(AddLocationViewModel model)
        {
            LocationId = Guid.NewGuid();
            Latitude = model.Latitude;
            Longitude = model.Longitude;
        }
    }
}
