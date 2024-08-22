using vPetz.Data.Services; // Correct namespace for IPetService

namespace vPetz.Data.BackgroundServices
{
    public class PetReplenishmentService : BackgroundService
    {
        private readonly IServiceProvider _provider;

        public PetReplenishmentService(IServiceProvider provider)
        {
            _provider = provider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _provider.CreateScope())
                {
                    var petService = scope.ServiceProvider.GetRequiredService<IPetService>();

                    // Call the method to decrease stats for all adopted pets
                    await DecreaseAdoptedPetsStatsAsync(petService, stoppingToken);

                    // Ensure there are enough pets available for adoption
                    await petService.EnsureMinimumPetsAvailableAsync();

                    // Wait for 10 seconds before the next update
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
            }
        }

        private async Task DecreaseAdoptedPetsStatsAsync(IPetService petService, CancellationToken stoppingToken)
        {
            var adoptedPets = await petService.GetAllAdoptedPetsAsync();

            foreach (var pet in adoptedPets)
            {
                pet.Happiness = Math.Max(0, pet.Happiness - 10);
                pet.Energy = Math.Max(0, pet.Energy - 15);
                pet.Cleanliness = Math.Max(0, pet.Cleanliness - 5);
                pet.Hunger = Math.Min(100, pet.Hunger + 8); // Hunger increases over time
                pet.Thirst = Math.Min(100, pet.Thirst + 12); // Thirst increases over time

                await petService.UpdatePetDetailsAsync(pet);

                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}