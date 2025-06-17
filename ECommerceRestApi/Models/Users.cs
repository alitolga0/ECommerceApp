using Microsoft.AspNetCore.Identity;

namespace ECommerceRestApi.Models
    {
        public class User : IdentityUser<Guid>
        {
            public string Name { get; set; } = null!;
            public string Surname { get; set; } = null!;
            public UserType? UserType { get; set; }

            public ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();
            public ICollection<Order>? Orders { get; set; } = new List<Order>();
            public ICollection<Product>? Products { get; set; } = new List<Product>();
        }
}


public enum UserType
{
    User,
    Admin
}