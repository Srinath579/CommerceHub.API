using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Domain.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Navigation property for the one-to-many relationship
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
