using System;
using System.ComponentModel.DataAnnotations;

namespace EbayAlike.Domain.Models
{
    public class Order : IEntity
    {
        [Key]
        public string Id { get; set; }
        [MaxLength(256)]
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double MoneyStep { get; set; }
        public TimeSpan TimeStep { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public ApplicationUser Seller { get; set; }
    }
}
