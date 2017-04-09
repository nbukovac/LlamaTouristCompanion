using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestBotApplication.Models
{
    public class AdministrationEventClass
    {
        public Guid ApartmentId { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public string Images { get; set; }
        public string Address { get; set; }
        public string Utilities { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public Guid OwnerId { get; set; }
    }
}