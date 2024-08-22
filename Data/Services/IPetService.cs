namespace vPetz.Data.Services
{
    public interface IPetService
    {
        Task<List<Pet>> GetAvailablePetsAsync();
        Task<List<Pet>> GetUserPetsAsync(string userId);
        Task AdoptPetAsync(Pet pet, string username);
        Task EnsureMinimumPetsAvailableAsync();
        Task<Pet> GetPetByIdAsync(int petId);
        Task<bool> DeletePetAsync(int petId);
        Task UpdatePetDetailsAsync(Pet pet);

        // New methods for pet interactions
        void PetAction(Pet pet);
        void FeedAction(Pet pet);
        void BrushAction(Pet pet);
        void WaterAction(Pet pet);
        void ExerciseAction(Pet pet);

        // Negative actions for testing
        Task MakeSadAsync(Pet pet);
        Task MakeHungryAsync(Pet pet);
        Task MakeMessyAsync(Pet pet);
        Task MakeThirstyAsync(Pet pet);
        Task MakeAwakeAsync(Pet pet);

        // Method to get all adopted pets
        Task<List<Pet>> GetAllAdoptedPetsAsync();

        string GetPetFeeling(Pet pet);
    }
}
