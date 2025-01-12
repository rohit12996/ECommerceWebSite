namespace ECommerceWebApi.Models
{
    public class Order
    {
        [Key]
        public int ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string? price { get; set; }

        public string Category { get; set; } = null!;

        public string Quantity { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int userId { get; set; }
       
    }
}
