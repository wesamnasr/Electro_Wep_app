using MY_API_PROJECT;


namespace MY_API_PROJECT.DTO.OrderItemDTOs
{
    public class OrderItemDTO
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; } 
        public int Quantity { get; set; } 
        public decimal UnitPrice { get; set; } 
        public decimal TotalPrice => Quantity * UnitPrice; 
    }
}