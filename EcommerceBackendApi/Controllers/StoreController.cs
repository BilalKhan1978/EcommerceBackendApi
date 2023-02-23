using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace EcommerceBackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly ILogger<StoreController> _logger;
        public StoreController(IStoreService storeService, ILogger<StoreController> logger)
        {
            _storeService = storeService;
            _logger = logger;   
        }

        //[HttpPost]
        //[Authorize(Roles = "super-admin")]
        //public async Task<IActionResult> AddStores([FromBody] List<AddStoreRequestDto> addStoreRequestDto)
        //{
        //  try
        //  {
        //      await _storeService.AddStores(addStoreRequestDto);
        //        return Ok("All stores have been added");
        //  }
        //  catch(Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        [HttpPost("store")]
        [Authorize(Roles = "super-admin")]
        public async Task<IActionResult> AddStore([FromBody] AddStoreRequestDto addStoreRequestDto)
        {
            try
            { 
                await _storeService.AddStore(addStoreRequestDto);
                return Ok("New store has been added");
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "super-admin")]
        public async Task<IActionResult> GetAllStoresData()
        {
          try
          {
              return Ok(await _storeService.GetAllStoresData());
          }
          catch(Exception e)
          {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
          }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "super-admin")]
        public async Task<IActionResult> GetStoreById([FromRoute] int id)
        {
            try
            {
                return Ok(await _storeService.GetStoreById(id));
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                if (e.Message.Contains("No Record Found"))
                   {
                    return NotFound("No Record Found to Display");
                   }
                throw new Exception(e.Message);
            }
        }

        //[HttpGet("uniquestoreid/{uniqueStoreId}")]
        //[Authorize(Roles = "super-admin")]
        //public async Task<IActionResult> GetStoreByUniqueId([FromRoute] int uniqueStoreId)
        //{
        //    try
        //    {
        //       return Ok(await _storeService.GetStoreByUniqueId(uniqueStoreId));    
        //    }
        //    catch(Exception e)
        //    {
        //        if (e.Message.Contains("No Record Found"))
        //        {
        //            return NotFound("No Record Found to Display");
        //        }
        //        throw new Exception(e.Message);
        //    }
        //}

        [HttpDelete("{id}")]
        [Authorize(Roles = "super-admin")]
        public async Task<IActionResult> DeleteStoreById([FromRoute] int id)
        {
            try
            { 
                await _storeService.DeleteStoreById(id);
                return Ok("Desired store has been deleted along with their products");
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                if (e.Message.Contains("No Record Found"))
                {
                    NotFound("No record found to delete / remove");
                }
                throw new Exception(e.Message); 
            }
        }

        //[HttpDelete("uniquestoreid/{uniqueStoreId}")]
        //[Authorize(Roles = "super-admin")]
        //public async Task<IActionResult> DeleteStoreByUniqueId([FromRoute] int uniqueStoreId)
        //{
        //    try
        //    { 
        //        await _storeService.DeleteStoreByUniqueId(uniqueStoreId);
        //        return Ok("Desired store has been deleted along with their products");
        //    }
        //    catch(Exception e)
        //    {
        //        if (e.Message.Contains("No Record Found"))
        //        {
        //            NotFound("No store found to delete / remove");
        //        }
        //        throw new Exception(e.Message);
        //    }
        //}

        [HttpDelete("allstores")]
        [Authorize(Roles = "super-admin")]
        public async Task<IActionResult> DeleteAllStores()
        {
          try
          {
             await _storeService.DeleteAllStores();
             return Ok("All stores have been deleted");
          }
          catch (Exception e)
          {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
          }
        }

        [HttpGet("storeadminproducts")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetAllProductsByStoreAdmin()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var userClaims = identity.Claims;
            var emailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
            try
            {
                return Ok(await _storeService.GetStoreWithProductsByAdminEmail(emailAddress));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                if (e.Message.Contains("User not found"))
                    NotFound("User does not exist");
                if (e.Message.Contains("Store not found"))
                    NotFound("Store does not exist");
                throw new Exception(e.Message);
            }
        }

        [HttpGet("search/{query}")]
        public async Task<IActionResult> SearchStores([FromRoute] string query)
        {
            try
            {
                return Ok(await _storeService.SearchStores(query));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
