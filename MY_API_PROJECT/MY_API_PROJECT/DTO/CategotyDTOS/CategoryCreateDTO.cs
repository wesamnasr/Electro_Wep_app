using System.ComponentModel.DataAnnotations;

namespace MY_API_PROJECT.DTO.CategotyDTOS
{
    public class CategoryCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }
    }

}
