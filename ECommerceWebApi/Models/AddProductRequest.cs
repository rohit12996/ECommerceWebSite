namespace ECommerceWebApi.Models
{
    public class AddProductRequest
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; } = null!;

        public int StockQuantity { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int SellerId { get; set; }

        public List<string> ImagePath { get; set; } = null!;
    }
}
