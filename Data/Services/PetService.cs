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
            // Simply fetch available pets without checking to create new ones here.
            return await _context.Pets.Where(p => !p.IsAdopted).ToListAsync();
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
                var pet = new Pet
                {
                    Name = $"Pet {random.Next(1000, 9999)}",
                    Age = random.Next(1, 10),
                    Type = (PetType)random.Next(0, 5),
                    Happiness = 50,
                    Sleepiness = 0,
                    Cleanliness = 70,
                    Hunger = 50,
                    Thirst = 50,
                    IsAdopted = false,
                    Description = "A lovely pet looking for a home.",
                    ImageURL = "/images/default-pet.png"
                };
                _context.Pets.Add(pet);
            }
            await _context.SaveChangesAsync();
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
    }
}
