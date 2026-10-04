using System;
using System.Collections.Generic;
using System.Text;

namespace CommerceHub.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
