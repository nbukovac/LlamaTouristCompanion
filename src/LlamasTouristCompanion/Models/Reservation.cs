﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LlamasTouristCompanion.Models
{
    public class Reservation
    {
        public Guid ReservationId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public Guid ApartmentId { get; set; }

        public virtual Apartment Apartment { get; set; }

        public Reservation(DateTime startDate, DateTime endDate, Guid apartmentId)
        {
            ReservationId = Guid.NewGuid();
            StartDate = startDate;
            EndDate = endDate;
            ApartmentId = apartmentId;
        }

        public Reservation()
        {
            
        }
    }
}
