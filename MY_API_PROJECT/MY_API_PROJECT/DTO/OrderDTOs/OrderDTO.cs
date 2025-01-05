using MY_API_PROJECT.DTO.OrderItemDTOs;

namespace MY_API_PROJECT.DTO.OrderDTOs
{
    public class OrderDTO
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}