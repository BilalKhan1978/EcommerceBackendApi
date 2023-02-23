using EcommerceBackendApi.Models;
using EcommerceBackendApi.ViewModels;

namespace EcommerceBackendApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<GetProductsRequestDto>> GetAllProducts(int offset, int count);
        Task AddProduct(AddProductRequestDto addProductRequestDto);
        //Task AddProducts(List<AddProductRequestDto> addProductRequestDto);
        Task<List<GetProductsRequestDto>> GetProductsByUniqueStoreId(int uniqueStoreId);
        Task<GetProductsRequestDto> GetProductById(int id);
        Task UpdateProduct(UpdateProductRequestDto updateProductRequestDto);
        Task DeleteProductById(int id);
        Task DeleteProductsByUniqueStoreId(int uniqueStoreId);
        Task<List<Product>> SearchProducts(string searchCriteria);


    }
}
