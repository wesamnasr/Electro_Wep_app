using MY_API_PROJECT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Review
{
    [Key]
    public int ID { get; set; }
    public int ProductID { get; set; }  // Foreign Key to Products
    public string UserID { get; set; }  // Foreign Key to Users
    public int Rating { get; set; }
    public string Comment { get; set; }= string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;


    [ForeignKey("ProductID")]
    public virtual Product? Product { get; set; }  // Navigation property


    [ForeignKey("UserID")]
    public virtual ApplicationUser? User { get; set; }  // Navigation property
   
}