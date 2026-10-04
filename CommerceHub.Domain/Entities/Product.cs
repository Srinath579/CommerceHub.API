using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Domain.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }

        // Navigation property (assuming Category entity exists later)
        // public Category Category { get; set; } 
    }
}
