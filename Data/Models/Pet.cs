using System.ComponentModel.DataAnnotations;
using vPetz.Data.Models.Enums; // This imports the PetType enum

namespace vPetz.Data
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        public int Age { get; set; }

        [Required]
        public PetType Type { get; set; }

        [Range(0, 100)]
        public double Happiness { get; set; } = 50; // Default to 50

        [Range(0, 100)]
        public double Energy { get; set; } = 100; // Default to 100

        [Range(0, 100)]
        public double Cleanliness { get; set; } = 70; // Default to 70

        [Range(0, 100)]
        public double Hunger { get; set; } = 50; // Default to 50

        [Range(0, 100)]
        public double Thirst { get; set; } = 50; // Default to 50

        public bool IsAdopted { get; set; } = false; // Default to false, indicating the pet is not available for interactions until adopted.

        public DateTime AdoptionDate { get; set; }

        [StringLength(500)]
        public string Description { get; set; } = string.Empty;

        public string ImageURL { get; set; } = "/images/default-cat.webp"; // Default image URL

        // Foreign key for ApplicationUser
        public string? OwnerId { get; set; }
        public ApplicationUser? Owner { get; set; }

        // Navigation property for users who love this pet
        public ICollection<UserPetLove> LovedByUsers { get; set; } = new List<UserPetLove>();

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

    }
}