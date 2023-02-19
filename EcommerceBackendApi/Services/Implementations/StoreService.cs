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
        
        public async Task AddStores(List<AddStoreRequestDto> addStoreRequestDto)
        {
            var stores = _mapper.Map<List<Store>>(addStoreRequestDto);
            await _dbContext.Stores.AddRangeAsync(stores);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<GetStoreRequestDto>> GetAllStoresData()
        {
            var stores = await _dbContext.Stores.ToListAsync();
            var storesDto = _mapper.Map<List<GetStoreRequestDto>>(stores);
            return storesDto;
        }
        public async Task<GetStoreRequestDto> GetStoreById(int id)
        {
            var store = await _dbContext.Stores.FindAsync(id);
            if (store == null) throw new Exception("No Record Found");
            var storeDto = _mapper.Map<GetStoreRequestDto>(store);
            return storeDto;

        }
    }
}
