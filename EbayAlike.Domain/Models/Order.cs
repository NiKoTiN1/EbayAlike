using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EbayAlike.Domain.Models
{
    public class Order
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
