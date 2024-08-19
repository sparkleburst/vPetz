using System.ComponentModel.DataAnnotations;

namespace vPetz.Data
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public int Price { get; set; }

        // Optional: Category to help organize items (e.g., Food, Toys, Care)
        public ItemType Type { get; set; }

        public string ImageURL { get; set; } = string.Empty;

        // Quantity in stock (if you plan to manage inventory)
        public int StockQuantity { get; set; }
    }
}