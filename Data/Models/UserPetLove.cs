using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vPetz.Data
{
    public class UserPetLove
    {
        [Key]
        public int Id { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int PetId { get; set; }
        public Pet? Pet { get; set; }

        public DateTime LikedOn { get; set; } = DateTime.UtcNow;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}