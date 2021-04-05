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
        Task<List<Pet>> GetPet(int id);
        Task<ApiResponse> UpdatePet(int id, string name, PetStatus status);
        Task<Pet> UpdatePet(Pet updatedPet);
        Task<ApiResponse> UploadPetImage(int petId, string additionalMetaData, byte[] file);
        Task<ApiResponse> DeletePet(int id);

        #endregion
    }
}
