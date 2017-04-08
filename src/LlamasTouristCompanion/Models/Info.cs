using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class Info
    {
        public Guid InfoId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public Guid LocationId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual Location Location { get; set; }
    }
}
