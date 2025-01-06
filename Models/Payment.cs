using MY_API_PROJECT.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Payment
{
    [Key]
    public int ID { get; set; }
    public int OrderID { get; set; }  // Foreign Key to Orders
    public decimal Amount { get; set; } = 1;
    public string PaymentMethod { get; set; }
    public string PaymentStatus { get; set; }
    public DateTime CreatedAt { get; set; }= DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;


    [ForeignKey("OrderID")]
    public virtual Order? Order { get; set; }  // Navigation property
}
