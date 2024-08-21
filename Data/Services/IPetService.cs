namespace vPetz.Data.Services
{
    public interface IPetService
    {
        Task<List<Pet>> GetAvailablePetsAsync();
        Task<List<Pet>> GetUserPetsAsync(string userId);
        Task AdoptPetAsync(Pet pet, string username);
        Task EnsureMinimumPetsAvailableAsync();
        Task<Pet> GetPetByIdAsync(int petId);
    }
}