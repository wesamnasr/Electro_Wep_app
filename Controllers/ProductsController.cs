using Microsoft.AspNetCore.Mvc;
using MY_API_PROJECT.DTO.productDTOS;
using MY_API_PROJECT.Models;
using MY_API_PROJECT.Repositories.Interfaces;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductRepository productRepository,ILogger<ProductController> logger)
    {
        _productRepository = productRepository;
        this. _logger = logger;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
    {
        try
        {
            var products = await _productRepository.GetAllProductsAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }
    }

    // GET: api/Product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDetailsDTO>> GetProduct(int id)
    {
        try
        {
            _logger.LogInformation("Getting product #{id} ",id);
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                _logger.LogWarning(" product # {id} was not found in date => {d} ", id,DateTime.Now);
                return NotFound(new { Message = $"Product with ID {id} not found." });

            }

            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }
    }

    // POST: api/Product
    [HttpPost]
    public async Task<ActionResult<ProductDTO>> AddProduct([FromBody] ProductCreateDTO productDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var product = new Product
            {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Price = productDTO.Price,
                StockQuantity = productDTO.StockQuantity,
                CategoryID = productDTO.CategoryId
            };

            await _productRepository.AddProductAsync(product);

            return CreatedAtAction(nameof(GetProduct), new { id = product.ID }, product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }
    }

    // PUT: api/Product/5
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO productUpdateDTO)
    {
        if (id != productUpdateDTO.Id)
        {
            return BadRequest(new { Message = "ID in URL and body do not match." });
        }

        try
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            var product = new Product
            {
                ID = productUpdateDTO.Id,
                Name = productUpdateDTO.Name,
                Description = productUpdateDTO.Description,
                Price = productUpdateDTO.Price,
                StockQuantity = productUpdateDTO.StockQuantity,
                CategoryID = productUpdateDTO.CategoryId
            };
            await _productRepository.UpdateProductAsync(product);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }
    }

    // DELETE: api/Product/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            var existingProduct = await _productRepository.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found." });
            }

            await _productRepository.DeleteProductAsync(id);

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = ex.Message });
        }
    }
}
