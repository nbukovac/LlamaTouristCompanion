using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class Guest
    {
        public Guid GuestId { get; set; }
        [Required]
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string IdType { get; set; }
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public Guid UserId { get; set; }

        public Guest(Guid guestId, string name, string identificationNumber, string idType, string phone, string email, Guid userId)
        {
            GuestId = guestId;
            Name = name;
            IdentificationNumber = identificationNumber;
            IdType = idType;
            Phone = phone;
            Email = email;
            UserId = userId;
        }

        public Guest()
        {
            
        }
    }
}
