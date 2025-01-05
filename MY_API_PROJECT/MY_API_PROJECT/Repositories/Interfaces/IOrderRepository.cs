using MY_API_PROJECT.DTO.OrderDTOs;
using MY_API_PROJECT.Models;

namespace MY_API_PROJECT.Interfaces
{
    public interface IOrderRepository
    {
    
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();

      
        Task<OrderDTO?> GetOrderByIdAsync(int orderId);

       
        Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(string userId);


        Task<Order> CreateOrderAsync(OrderCreateDTO orderCreateDto);


        Task<Order?> UpdateOrderAsync(int orderId, OrderUpdateDTO orderUpdateDto);


        Task<bool> DeleteOrderAsync(int orderId);
    }
}
