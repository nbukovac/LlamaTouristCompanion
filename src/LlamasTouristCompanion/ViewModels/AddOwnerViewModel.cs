using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.ViewModels
{
    public class AddOwnerViewModel
    {
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


        public AddOwnerViewModel(string name, string phone, string email, string face, string twitt, 
            string inst, string tube)
        {
            Name = name;
            Phone = phone;
            Email = email;
            FacebookUrl = face;
            TwitterUrl = twitt;
            InstagramUrl = inst;
            YoutubeUrl = tube;
        }
    }
}
