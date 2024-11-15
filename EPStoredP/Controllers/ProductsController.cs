using EPStoredP.Entities;
using EPStoredP.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EPStoredP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }
        [HttpGet("getproductlist")]
        public async Task<IActionResult> GetProductListAsync()
        {
            try
            {
              var product = await productService.GetProductListAsync();
                return StatusCode(StatusCodes.Status200OK, product);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while retrieving the product. Please try again later." });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductByIdAsync(int Id)
        {
            try
            {
                var response = await productService.GetProductByIdAsync(Id);
                if (response == null)
                {
                   return StatusCode(StatusCodes.Status404NotFound, new { Message = $"Product with ID {Id} not found." });
                }

                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while retrieving the product. Please try again later." });
            }
        }


        [HttpPost("addproduct")]
        public async Task<IActionResult> AddProductAsync(Product product)
        {
            if (product == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { Message = $"invalid product." });
            }
            try
            {
                var response = await productService.AddProductAsync(product);
                return StatusCode(StatusCodes.Status200OK, "ProductSuccessfully Added");
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while retrieving the product. Please try again later." });
            }
        }
        [HttpPut("updateproduct")]
        public async Task<IActionResult> UpdateProductAsync(Product product)
        {
            if (product == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid product ");
            }
            try
            {
                var result = await productService.UpdateProductAsync(product);
                return StatusCode (StatusCodes.Status200OK, "Product updated successfully");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while retrieving the product. Please try again later." });
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProductAsync(int id)
        {
            try
            {
          
                var productToDelete = await productService.DeleteProductAsync(id);

                if (productToDelete != null)
                {

                    return NotFound($"product with Id = {id} not found");
                }
                    return StatusCode(StatusCodes.Status200OK, productToDelete);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An error occurred while retrieving the product. Please try again later." });
            }
        }
    }
}
