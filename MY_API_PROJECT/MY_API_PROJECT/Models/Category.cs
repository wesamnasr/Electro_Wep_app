
using MY_API_PROJECT.Models;
using System.ComponentModel.DataAnnotations;

public class Category
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; }= string.Empty;
   // public int? ParentCategoryID { get; set; }  // Nullable foreign key for self-referencing
    public string Description { get; set; }=string.Empty;
    public DateTime CreatedAt { get; set; }=DateTime.Now;

   // public virtual Category? ParentCategory { get; set; }  // Navigation property for subcategories
   // public virtual ICollection<Category>? SubCategories { get; set; }  // Navigation property for subcategories
    public virtual ICollection<Product>? Products { get; set; } 
}
