using EcommerceBackendApi.Models;
using EcommerceBackendApi.ViewModels;

namespace EcommerceBackendApi.Services.Interfaces
{
    public interface IStoreService
    {
        Task AddStore(AddStoreRequestDto addStoreRequestDto);
        //Task AddStores(List<AddStoreRequestDto> addStoreRequestDto);
        Task<List<GetStoreRequestDto>> GetAllStoresData();
        Task<GetStoreRequestDto> GetStoreById(int id);
        //Task<GetStoreRequestDto> GetStoreByUniqueId(int uniqueStoreId);
        Task DeleteStoreById(int id);
        //Task DeleteStoreByUniqueId(int uniqueStoreId);
        Task DeleteAllStores();
        Task<StoreProductsRequestDto> GetStoreWithProductsByAdminEmail(string email);
        Task<List<Store>> SearchStores(string searchCriteria);
    }
}
