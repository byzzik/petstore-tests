namespace PetStoreTests.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;

    public interface IPetStoreClient
    {
        #region Methods

        Task<Pet> AddPet(Pet newPet);
        Task<List<Pet>> GetPet(PetStatus status);
        Task<List<Pet>> GetPet(ulong? id);
        Task<ApiResponse> UpdatePet(ulong? id, string name, PetStatus status);
        Task<Pet> UpdatePet(Pet updatedPet);
        Task<ApiResponse> DeletePet(ulong? id);

        #endregion
    }
}
