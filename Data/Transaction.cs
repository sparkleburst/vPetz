using System.ComponentModel.DataAnnotations;
using vPetz.Data;

namespace vPetz.Data
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int ItemId { get; set; }
        public Item? Item { get; set; }

        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;

        public int Quantity { get; set; }

        // Calculated property to get total cost
        public decimal TotalCost => Quantity * (Item?.Price ?? 0);
    }
}