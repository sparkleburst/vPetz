using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace vPetz.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{

    [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters.")]
    public String? FirstName { get; set; }

    [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
    public String? LastName { get; set; }

    public int Coins { get; set; } = 10;  // Default starting coins

    // Navigation property for pets owned by the user
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();

    // Navigation property for pets loved by the user
    public ICollection<UserPetLove> LovedPets { get; set; } = new List<UserPetLove>();

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

}

