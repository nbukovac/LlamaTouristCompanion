using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.ViewModels
{
    public class AddLocationViewModel
    {
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }

        public AddLocationViewModel(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
