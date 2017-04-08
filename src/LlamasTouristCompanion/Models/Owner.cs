using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class Owner
    {

        public Guid OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Url]
        public string FacebookUrl { get; set; }
        [Url]
        public string TwitterUrl { get; set; }
        [Url]
        public string InstagramUrl { get; set; }
        [Url]
        public string YoutubeUrl { get; set; }

        public Guid UserId { get; set; }

        public virtual List<Apartment> Apartments { get; set; }


        public Owner()
        {
            
        }

        public Owner(string name, string phone, string email, string facebookUrl, string twitterUrl, string instagramUrl, string youtubeUrl, Guid userId)
        {
            OwnerId = Guid.NewGuid();
            Name = name;
            Phone = phone;
            Email = email;
            FacebookUrl = facebookUrl;
            TwitterUrl = twitterUrl;
            InstagramUrl = instagramUrl;
            YoutubeUrl = youtubeUrl;
            UserId = userId;
        }
    }
}
