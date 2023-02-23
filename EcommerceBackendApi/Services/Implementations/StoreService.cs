using AutoMapper;
using EcommerceBackendApi.Data;
using EcommerceBackendApi.Models;
using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EcommerceBackendApi.Services.Implementations
{
    public class StoreService : IStoreService
    {
        // Inject the services
        public EcommerceDbContext _dbContext;
        IMapper _mapper;

        public StoreService(EcommerceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;   
        }
        
        //public async Task AddStores(List<AddStoreRequestDto> addStoreRequestDto)
        //{
        //    var stores = _mapper.Map<List<Store>>(addStoreRequestDto);
        //    await _dbContext.Stores.AddRangeAsync(stores);
        //    await _dbContext.SaveChangesAsync();
        //}
       
        public async Task  AddStore(AddStoreRequestDto addStoreRequestDto)
        {
            var store = _mapper.Map<Store>(addStoreRequestDto);
            await _dbContext.Stores.AddAsync(store);
            await _dbContext.SaveChangesAsync();
        }
       
        public async Task<List<GetStoreRequestDto>> GetAllStoresData()
        {
            var stores = await _dbContext.Stores.ToListAsync();
            return _mapper.Map<List<GetStoreRequestDto>>(stores);
        }

        public async Task<GetStoreRequestDto> GetStoreById(int id)
        {
            var store = await _dbContext.Stores.FindAsync(id);
            if (store == null) throw new Exception("No Record Found");
            return _mapper.Map<GetStoreRequestDto>(store);
        }

        //public async Task<GetStoreRequestDto> GetStoreByUniqueId(int uniqueStoreId)
        //{
        //    var store = await _dbContext.Stores.FindAsync(uniqueStoreId);
        //    if (store == null) throw new Exception("No record found");
        //    return _mapper.Map<GetStoreRequestDto>(store);
        //}

        public async Task DeleteStoreById(int id)
        {
            var store = await _dbContext.Stores.FindAsync(id);
            if (store == null) throw new Exception("No Record Found");
            
            var products = await _dbContext.Products.Where(x => x.UniqueStoreId == store.UniqueStoreId).ToListAsync();
            if (products != null)
            {
                _dbContext.Remove(products);
            }
            _dbContext.Stores.Remove(store);
            await _dbContext.SaveChangesAsync();
        }
       
        //public async Task DeleteStoreByUniqueId(int uniqueStoreId)
        //{
        //  var store = await _dbContext.Stores.FindAsync(uniqueStoreId);
        //  if (store == null) throw new Exception("No Record Found");
          
        //  var products = await _dbContext.Products.Where(x => x.UniqueStoreId == uniqueStoreId).ToListAsync();
        //    if (products != null)
        //    {
        //        _dbContext.Remove(products);
        //    }
        //    _dbContext.Stores.Remove(store);
        //    await _dbContext.SaveChangesAsync();

        //}

        public async Task DeleteAllStores()
        {
             _dbContext.Products.RemoveRange();
             _dbContext.Stores.RemoveRange();
             await _dbContext.SaveChangesAsync();
        }

        public async Task<StoreProductsRequestDto> GetStoreWithProductsByAdminEmail(string email)
        {
           var user = await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
           if (user == null) throw new Exception("User not found");

           var store = await _dbContext.Stores.Where(x => x.UniqueStoreId == user.UniqueStoreId).FirstOrDefaultAsync();
           if (store == null) throw new Exception("Store not found");

           var products = await _dbContext.Products.Where(x => x.UniqueStoreId == user.UniqueStoreId).ToListAsync();
                       
           return new StoreProductsRequestDto
            {
                StoreId = store.Id,
                UniqueStoreId = store.UniqueStoreId,
                Name = store.Name,
                ProductsList = products == null? null : _mapper.Map<List<GetProductsRequestDto>>(products)
            };
        }

        public async Task<List<Store>> SearchStores(string searchCriteria)
        {
            var trimmedQuery = "%" + searchCriteria + "%";
            var query = _dbContext.Stores
                            .Where(x => EF.Functions.Like(x.Name, trimmedQuery));
            var productsList = await query.ToListAsync();
            return productsList;
        }
    }
}
