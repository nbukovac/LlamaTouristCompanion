using System;
using System.ComponentModel.DataAnnotations;

namespace LlamasTouristCompanion.Models
{
    public class Price
    {
        public Guid PriceId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public Guid ApartmentId { get; set; }

        public Price()
        {

        }

        public Price(DateTime startDate, DateTime endDate, double amount, Guid apartmentId)
        {
            PriceId = Guid.NewGuid();
            StartDate = startDate;
            EndDate = endDate;
            Amount = amount;
            ApartmentId = apartmentId;
        }
    }
}
