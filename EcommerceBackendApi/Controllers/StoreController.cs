using EcommerceBackendApi.Services.Implementations;
using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost]
        public async Task<IActionResult> AddStores(List<AddStoreRequestDto> addStoreRequestDto)
        {
          try
          {
              await _storeService.AddStores(addStoreRequestDto);
                return Ok("All stores have been added");
          }
          catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllStoresData()
        {
          try
          {
              return Ok(await _storeService.GetAllStoresData());
          }
          catch(Exception e)
          {
                throw new Exception(e.Message);
          }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreById(int id)
        {
            try
            {
                var store = await _storeService.GetStoreById(id);
                return Ok(store);
            }
            catch(Exception e)
            { 
                if (e.Message.Contains("No Record Found"))
                   {
                    return NotFound("No Record Found to Display");
                   }
                throw new Exception(e.Message); 
            }
            
        }
    }
}
