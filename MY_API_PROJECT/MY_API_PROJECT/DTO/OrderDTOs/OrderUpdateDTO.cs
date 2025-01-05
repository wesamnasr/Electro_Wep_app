using MY_API_PROJECT.DTO.OrderItemDTOs;



    public class OrderUpdateDTO
    {


        public string Status { get; set; } 
        public decimal TotalAmount { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }

