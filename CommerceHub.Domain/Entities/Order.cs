using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Domain.Entities
{
    public class Order : BaseEntity
    {
        public Guid UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = "Pending"; // E.g., Pending, Shipped, Delivered

        // Navigation properties
        public User User { get; set; } = null!;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
