using MY_API_PROJECT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Product
{
    [Key]
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; } = 0;
    public int StockQuantity { get; set; } = 0;
    public int CategoryID { get; set; } = 1; // Foreign Key to Categories
    public DateTime CreatedAt { get; set; } = DateTime.Now;
   
   
    
    [ForeignKey("CategoryID")]
    public virtual Category? Category { get; set; }  // Navigation property
    public virtual ICollection<Order>? OrderItems { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }
    
}
