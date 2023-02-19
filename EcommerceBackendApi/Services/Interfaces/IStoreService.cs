using EcommerceBackendApi.Models;
using EcommerceBackendApi.ViewModels;

namespace EcommerceBackendApi.Services.Interfaces
{
    public interface IStoreService
    {
        Task<List<GetStoreRequestDto>> GetAllStoresData();
        Task AddStores(List<AddStoreRequestDto> addStoreRequestDto);
        Task<GetStoreRequestDto> GetStoreById(int id);
    }
}
