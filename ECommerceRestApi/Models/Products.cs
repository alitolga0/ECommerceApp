namespace ECommerceRestApi.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }

        public Guid UserId { get; set; }  
        public User? User { get; set; }


        public ICollection<CartItem>? CartItems { get; set; } = new List<CartItem>();

        public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}