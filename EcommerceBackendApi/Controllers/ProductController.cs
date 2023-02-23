using EcommerceBackendApi.Models;
using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace EcommerceBackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _product;  // Inject the service
        private readonly IUserService _userService;
        public ProductController(IProductService product, IUserService userService)
        {
            _product = product;
            _userService = userService;
        }

        [HttpGet("AllProducts")]
        public async Task<IActionResult> GetAllProducts(int offset, int count)
        {
          try
          {
            return Ok(await _product.GetAllProducts(offset, count));
          }
          catch (Exception e)
          {
            throw new Exception(e.Message);
          }
        }

        [HttpPost]
        [Authorize(Roles = "super-admin, admin")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequestDto addProductRequestDto)
        {
          try
          {
                await _product.AddProduct(addProductRequestDto);
                return Ok("Product has been added");
          }
          catch(Exception e)
          {
                throw new Exception(e.Message);
          }
        }

       // this is just for posting all data. 
        //[HttpPost("/api/Products")]
        //public async Task<IActionResult> AddProducts(List<AddProductRequestDto> addProductRequestDto)
        //{
        //   try
        //   { 
        //    await _product.AddProducts(addProductRequestDto);
        //    return Ok("Prodcuts have been added");
        //   }
        //   catch (Exception e)
        //   {
        //        throw new Exception(e.Message);
        //   }
        //}

        [HttpGet("uniqueStoreId/{uniqueStoreId}")]
        public async Task<IActionResult> GetProductsByUniqueStoreId([FromRoute] int uniqueStoreId)
        {
            try
            {
                var products = await _product.GetProductsByUniqueStoreId(uniqueStoreId);
                return Ok(products);
            }
            catch(Exception e)
            {
                if (e.Message.Contains("Record Not Found"))
                    return NotFound("No Record Found");
                throw new Exception(e.Message);
            }

        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
          try
          {
            var product = await _product.GetProductById(id);
            return Ok(product);
          }
          catch (Exception e)
          {
                if (e.Message.Contains("No Record Found"))
                    return NotFound("No Record Found");
                throw new Exception(e.Message);
          }
        }

        [HttpPut]
        [Authorize(Roles = "super-admin, admin")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequestDto updateProductRequestDto)
        {
            try
            { 
                await _product.UpdateProduct(updateProductRequestDto);
                return Ok("Desired product has been updated");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        [HttpDelete("storeid/{id}")]
        [Authorize(Roles = "super-admin, admin")]
        public async Task<IActionResult> DeleteProductById([FromRoute] int id)
        {
            try
            {
                await _product.DeleteProductById(id);
                return Ok("Desired product has been deleted");
            }
            catch (Exception e)
            {
                if (e.Message.Contains("No Record Found"))
                    return NotFound("No Record Found to Delete");
                throw new Exception(e.Message);
            }
        }

        [HttpDelete("storeuniqueid/{uniqueStoreId}")]
        [Authorize(Roles = "super-admin, admin")]
        public async Task<IActionResult> DeleteProductsByUniqueStoreId([FromRoute] int uniqueStoreId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var userClaims = identity.Claims;
            var role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value;
            if (role == "admin")
            {
                var emailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value;
                var user = await _userService.GetUserByEmail(emailAddress);
                if (user != null)
                {
                    if (user.UniqueStoreId != uniqueStoreId)
                        return Unauthorized("User is not alloewed to delete this stroes' products");
                }
                else
                    return NotFound("User doesnt exist");
            }            
            
            try
            {
                await _product.DeleteProductsByUniqueStoreId(uniqueStoreId);
                return Ok($"All Products have been deleted against {uniqueStoreId}");
            }
          catch (Exception e)
          {
                if (e.Message.Contains("No Record Found"))
                    NotFound("No Record Found to Delete");
                throw new Exception(e.Message); 
          }
        }

        [HttpGet("search/{query}")]
        public async Task<IActionResult> SearchProducts([FromRoute] string query)
        {
            try
            {
                return Ok(await _product.SearchProducts(query));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
