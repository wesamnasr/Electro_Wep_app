using Microsoft.AspNetCore.Mvc;
using MY_API_PROJECT.Models; 
using MY_API_PROJECT.Repositories.Interfaces;
using MY_API_PROJECT.DTO.CategotyDTOS;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    // GET: api/Categories
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
    {
        var categories = await _categoryRepository.GetAllCategoriesAsync();
        return Ok(categories);
    }

  
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDetailsDTO>> GetCategory(int id)
    {
        var category = await _categoryRepository.GetCategoryByIdAsync(id);

        if (category == null)
        {
            return NotFound(new { Message = $"Category with ID {id} not found." });
        }

        return Ok(category);
    }

  
    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> AddCategory([FromBody] CategoryCreateDTO categoryDTO)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var category = new Category
        {
            Name = categoryDTO.Name,
            Description = categoryDTO.Description
        };

        await _categoryRepository.AddCategoryAsync(category);

      
        return CreatedAtAction(nameof(GetCategory), new { id = category.ID }, new CategoryDTO
        {
            Id = category.ID,
            Name = category.Name,
            Description = category.Description
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryUpdateDTO categoryUpdateDTO)
    {
        if (id != categoryUpdateDTO.Id)
        {
            return BadRequest(new { Message = "ID in URL and body do not match." });
        }

        var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound(new { Message = $"Category with ID {id} not found." });
        }

        var category = new Category
        {
            ID = categoryUpdateDTO.Id,
            Name = categoryUpdateDTO.Name,
            Description = categoryUpdateDTO.Description
          
        };
        await _categoryRepository.UpdateCategoryAsync(category);




        return NoContent();
    }

   
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
        if (existingCategory == null)
        {
            return NotFound(new { Message = $"Category with ID {id} not found." });
        }

        await _categoryRepository.DeleteCategoryAsync(id);
        return NoContent();
    }
}
