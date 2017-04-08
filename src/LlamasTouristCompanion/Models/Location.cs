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
        [Required]
        public string Address { get; set; }

        public Location(double latitude, double longitude, string address)
        {
            LocationId = Guid.NewGuid();
            Latitude = latitude;
            Longitude = longitude;
            Address = address;
        }

        public Location()
        {
            
        }

        public Location(AddLocationViewModel model)
        {
            LocationId = Guid.NewGuid();
            Latitude = model.Latitude;
            Longitude = model.Longitude;
            Address = model.Address;
        }
    }
}
