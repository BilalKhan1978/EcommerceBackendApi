using EcommerceBackendApi.Models;
using EcommerceBackendApi.ViewModels;

namespace EcommerceBackendApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<GetProductsRequestDto>> GetAllProducts();
        Task AddProduct(AddProductRequestDto addProductRequestDto);
        Task AddProducts(List<AddProductRequestDto> addProductRequestDto);
        Task<List<GetProductsRequestDto>> GetProductsByUniqueStoreId(int uniqueStoreId);
        Task<GetProductsRequestDto> GetProductById(int id);
        Task DeleteProduct(int id, int uniqueStoreId);
        
    }
}
