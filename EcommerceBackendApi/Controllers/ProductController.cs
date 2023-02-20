using EcommerceBackendApi.Services.Interfaces;
using EcommerceBackendApi.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceBackendApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductService _product;  // Inject the service
        public ProductController(IProductService product)
        {
            _product = product;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
          try
          {
            return Ok(await _product.GetAllProducts());
          }
          catch (Exception e)
          {
            throw new Exception(e.Message);
          }
        }
        
        [HttpPost]
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
        [HttpPost("/api/Products")]
        public async Task<IActionResult> AddProducts(List<AddProductRequestDto> addProductRequestDto)
        {
           try
           { 
            await _product.AddProducts(addProductRequestDto);
            return Ok("Prodcuts have been added");
           }
           catch (Exception e)
           {
                throw new Exception(e.Message);
           }
        }

        [HttpGet("uniqueStoreId/{uniqueStoreId}")]
        public async Task<IActionResult> GetProductsByUniqueStoreId(int uniqueStoreId)
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
        public async Task<IActionResult> GetProductById(int id)
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

        [HttpDelete("{id}/storeid/{uniqueStoreId}")]
        public async Task<IActionResult> DeleteProduct(int id,  int uniqueStoreId)
        {
            try
            {
                await _product.DeleteProduct(id, uniqueStoreId);
                return Ok();
            }
            catch (Exception e)
            {
                if (e.Message.Contains("No Record Found"))
                    return NotFound("No Record Found to Delete");
                throw new Exception(e.Message);
            }
        }
    }
}
