namespace vPetz.Data.Services
{
    public interface IPetService
    {
        Task<List<Pet>> GetAvailablePetsAsync();
        Task AdoptPetAsync(Pet pet, string username);
        Task EnsureMinimumPetsAvailableAsync();
        Task<Pet> GetPetByIdAsync(int petId);
    }
}