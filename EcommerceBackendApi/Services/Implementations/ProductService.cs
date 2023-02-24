using AutoMapper;
using EcommerceBackendApi.Data;
using EcommerceBackendApi.Models;
using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace EcommerceBackendApi.Services.Implementations
{
    public class ProductService : IProductService
    {
        // Inject the services
        public readonly EcommerceDbContext _dbContext;  
        IMapper _mapper;
        public ProductService(EcommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task AddProduct(AddProductRequestDto addProductRequestDto)
        {
            var product = _mapper.Map<Product>(addProductRequestDto);
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        //public async Task AddProducts(List<AddProductRequestDto> addProductRequestDto)
        //{
        //   var products = _mapper.Map<List<Product>>(addProductRequestDto);
        //    await _dbContext.Products.AddRangeAsync(products);
        //    await _dbContext.SaveChangesAsync();
        //}

        public async Task<List<GetProductsRequestDto>> GetAllProducts(int offset, int count)
        {
            var products = await _dbContext.Products.OrderBy(x => x.Id).Skip(offset).Take(count).ToListAsync();
            var productsDto = _mapper.Map<List<GetProductsRequestDto>>(products);
            return productsDto;

        }

        public async Task<List<GetProductsRequestDto>> GetProductsByUniqueStoreId(int uniqueStoreId)
        {
            //var storeProducts = await _dbContext.Products.FindAsync(UniqueStoreId);

           var storeProducts = await _dbContext.Products.Where(x => x.UniqueStoreId == uniqueStoreId).ToListAsync();
            if (storeProducts == null) throw new Exception("No Record Found");
            var storeProductsDto = _mapper.Map<List<GetProductsRequestDto>>(storeProducts);
            return storeProductsDto; 
        }

        public async Task<GetProductsRequestDto> GetProductById(int id)
        {
            var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product == null) throw new Exception("No Record Found");
            var productDto = _mapper.Map<GetProductsRequestDto>(product);
            return productDto;
        }
        
        public async Task UpdateProduct(UpdateProductRequestDto updateProductRequestDto)
        {
             
           var updateProduct = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == updateProductRequestDto.Id);
           if (updateProduct == null) throw new Exception("Product does not exist anymore");
           var product = _mapper.Map<Product>(updateProductRequestDto);
           _dbContext.Products.Update(product);    
           await _dbContext.SaveChangesAsync();
            
        }

        public async Task DeleteProductById(int id)
        {
            var product = await _dbContext.Products.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null) throw new Exception("No Record Found");
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

        }

        public async Task DeleteProductsByUniqueStoreId(int uniqueStoreId)
        {
            var storeProducts = await _dbContext.Products.Where(x => x.UniqueStoreId == uniqueStoreId).ToListAsync();
            if (storeProducts == null) throw new Exception("No Record Found");
            _dbContext.Remove(storeProducts);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> SearchProducts(string searchCriteria)
        {
            var trimmedQuery = "%" + searchCriteria + "%";
            var query = _dbContext.Products
                            .Where(x => EF.Functions.Like(x.Title, trimmedQuery) ||
                                        EF.Functions.Like(x.Category, trimmedQuery))
                            .OrderBy(x => x.Title);

            var productsList = await query.ToListAsync();
            return productsList;
        }
    }
}
