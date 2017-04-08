using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.ViewModels
{
    public class AddApartmentViewModel
    {
        public string Name { get; set; }
        [Required]
        public Guid LocationId { get; set; }
        [Required]
        public string Images { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Utilities { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public AddApartmentViewModel(string images, string address, string utilities, 
            DateTime checkIn, DateTime checkOut, string locationId, string name)
        {
            Name = name;
            LocationId = Guid.Parse(locationId);
            Address = address;
            Utilities = utilities;
            CheckIn = checkIn;
            CheckOut = checkOut;
            Images = images;
        }

        public AddApartmentViewModel()
        {

        }
    }
}
