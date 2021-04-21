namespace PetStoreTests.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;

    public interface IPetStoreClient
    {
        #region Methods

        Task<Order> AddOrder(Order order);
        Task<Pet> AddPet(Pet newPet);
        Task<ApiResponse> DeleteOrder(ulong? orderId);
        Task<ApiResponse> DeletePet(ulong? id);
        Task<Inventory> GetInventories();
        Task<Order> GetOrder(ulong? orderId);
        Task<Pet> GetPetById(ulong? id);
        Task<List<Pet>> GetPetByStatus(PetStatus status);
        Task<ApiResponse> UpdatePet(ulong? id, string name, PetStatus status);
        Task<Pet> UpdatePet(Pet updatedPet);

        #endregion
    }
}
