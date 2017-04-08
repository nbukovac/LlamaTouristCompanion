using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class Apartment
    {
        public Guid ApartmentId { get; set; }
        [Required]
        public string Name  { get; set; }
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
        public Guid OwnerId { get; set; }

        public virtual Location Location { get; set; }
        public virtual Owner Owner { get; set; }

        public Apartment(Guid apartmentId, string name, Guid locationId, string images, string address, string utilities, DateTime checkIn, DateTime checkOut, Guid ownerId)
        {
            ApartmentId = apartmentId;
            Name = name;
            LocationId = locationId;
            Images = images;
            Address = address;
            Utilities = utilities;
            CheckIn = checkIn;
            CheckOut = checkOut;
            OwnerId = ownerId;
        }

        public Apartment()
        {
            
        }
    }
}
