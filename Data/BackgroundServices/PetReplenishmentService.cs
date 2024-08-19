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
                    await petService.EnsureMinimumPetsAvailableAsync();
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);  // Check every hour
                }
            }
        }
    }
}
