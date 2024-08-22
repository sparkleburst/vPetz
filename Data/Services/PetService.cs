using Microsoft.EntityFrameworkCore;
using vPetz.Data.Models.Enums;

namespace vPetz.Data.Services
{
    public class PetService : IPetService
    {
        private readonly ApplicationDbContext _context;

        public PetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pet>> GetAvailablePetsAsync()
        {
            return await _context.Pets.Where(p => !p.IsAdopted).ToListAsync();
        }

        public async Task<List<Pet>> GetUserPetsAsync(string userId)
        {
            return await _context.Pets.Where(p => p.OwnerId == userId).ToListAsync();
        }

        public async Task AdoptPetAsync(Pet pet, string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null && pet != null)
            {
                pet.IsAdopted = true;
                pet.OwnerId = user.Id;
                await _context.SaveChangesAsync();
            }
        }

        public async Task EnsureMinimumPetsAvailableAsync()
        {
            var petsCount = await _context.Pets.CountAsync(p => !p.IsAdopted);
            if (petsCount < 12)
            {
                int petsToCreate = 12 - petsCount;
                await CreatePetsAsync(petsToCreate);
            }
        }

        private async Task CreatePetsAsync(int count)
        {
            var random = new Random();
            for (int i = 0; i < count; i++)
            {
                var petType = (PetType)random.Next(0, 5); // Randomly selecting a PetType

                var pet = new Pet
                {
                    Name = $"Pet {random.Next(1000, 9999)}",
                    Age = random.Next(1, 10),
                    Type = petType, // Set the pet type
                    Happiness = 50,
                    Energy = 100,
                    Cleanliness = 70,
                    Hunger = 50,
                    Thirst = 50,
                    IsAdopted = false,
                    Description = $"What a cute {petType.ToString().ToLower()}!",
                    ImageURL = GetImageUrlForPetType(petType) // Assign the image URL based on the pet type
                };
                _context.Pets.Add(pet);
            }
            await _context.SaveChangesAsync();
        }

        private string GetImageUrlForPetType(PetType petType)
        {
            return petType switch
            {
                PetType.Cat => "/images/default-cat.webp",
                PetType.Dog => "/images/default-dog.webp",
                PetType.Bird => "/images/default-bird.webp",
                PetType.Fish => "/images/default-fish.webp",
                PetType.Reptile => "/images/default-reptile.png",
                _ => "/images/default-cat.webp" // Fallback to the default-cat image if the type doesn't match
            };
        }

        public async Task<Pet> GetPetByIdAsync(int petId)
        {
            var pet = await _context.Pets
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null)
            {
                throw new InvalidOperationException("No pet found with the specified ID.");
            }

            return pet;
        }

        public async Task<bool> DeletePetAsync(int petId)
        {
            var pet = await _context.Pets.FindAsync(petId);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task UpdatePetDetailsAsync(Pet pet)
        {
            var existingPet = await _context.Pets.FindAsync(pet.Id);
            if (existingPet != null)
            {
                existingPet.Name = pet.Name;
                existingPet.Description = pet.Description;
                existingPet.Happiness = pet.Happiness;
                existingPet.Energy = pet.Energy;
                existingPet.Cleanliness = pet.Cleanliness;
                existingPet.Hunger = pet.Hunger;
                existingPet.Thirst = pet.Thirst;
                await _context.SaveChangesAsync();
            }
        }

        public void PetAction(Pet pet)
        {
            pet.Happiness = Math.Min(100, pet.Happiness + 10);
        }

        public void FeedAction(Pet pet)
        {
            pet.Hunger = Math.Max(0, pet.Hunger - 20);
        }

        public void BrushAction(Pet pet)
        {
            pet.Cleanliness = Math.Min(100, pet.Cleanliness + 15);
        }

        public void WaterAction(Pet pet)
        {
            pet.Thirst = Math.Max(0, pet.Thirst - 20);
        }

        public void ExerciseAction(Pet pet)
        {
            pet.Energy = Math.Max(0, pet.Energy - 15);
        }

        public async Task MakeSadAsync(Pet pet)
        {
            pet.Happiness = Math.Max(0, pet.Happiness - 30);
            await UpdatePetDetailsAsync(pet);
        }

        public async Task MakeHungryAsync(Pet pet)
        {
            pet.Hunger = Math.Min(100, pet.Hunger + 30);
            await UpdatePetDetailsAsync(pet);
        }

        public async Task MakeMessyAsync(Pet pet)
        {
            pet.Cleanliness = Math.Max(0, pet.Cleanliness - 30);
            await UpdatePetDetailsAsync(pet);
        }

        public async Task MakeThirstyAsync(Pet pet)
        {
            pet.Thirst = Math.Min(100, pet.Thirst + 30);
            await UpdatePetDetailsAsync(pet);
        }

        public async Task MakeAwakeAsync(Pet pet)
        {
            pet.Energy = Math.Min(100, pet.Energy + 30);
            await UpdatePetDetailsAsync(pet);
        }

        public async Task<List<Pet>> GetAllAdoptedPetsAsync()
        {
            return await _context.Pets
            .Include(p => p.Owner)
            .Where(p => p.IsAdopted)
            .ToListAsync();
        }

        public string GetPetFeeling(Pet pet)
        {
            if (pet == null) return "neutral";

            if (pet.Happiness > 80 && pet.Energy > 70 && pet.Hunger < 30 && pet.Thirst < 30)
            {
                return "happy and energetic";
            }
            else if (pet.Happiness < 50 || pet.Energy < 50 || pet.Hunger > 70 || pet.Thirst > 70)
            {
                return "a bit down and tired";
            }
            else if (pet.Hunger > 70 || pet.Thirst > 70)
            {
                return "hungry or thirsty";
            }
            else if (pet.Cleanliness < 50)
            {
                return "a bit dirty";
            }
            else
            {
                return "okay";
            }
        }
    }
}
