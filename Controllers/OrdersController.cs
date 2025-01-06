using Microsoft.AspNetCore.Mvc;
using MY_API_PROJECT.DTO;
using MY_API_PROJECT.DTO.OrderDTOs;
using MY_API_PROJECT.Interfaces;
using MY_API_PROJECT.Models;
using MY_API_PROJECT.Repositories;

namespace MY_API_PROJECT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Create Order
        [HttpPost("create")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderCreateDTO orderCreateDTO)
        {
            if (orderCreateDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            var createdOrder = await _orderRepository.CreateOrderAsync(orderCreateDTO);

            if (createdOrder == null)
            {
                return BadRequest("Failed to create order.");
            }

            return Ok(new { Message = "Order created successfully", Order = createdOrder });
        }

      
        [HttpPut("update/{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] OrderUpdateDTO orderUpdateDTO)
        {
            if (orderUpdateDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedOrder = await _orderRepository.UpdateOrderAsync(orderId, orderUpdateDTO);

            if (updatedOrder == null)
            {
                return NotFound("Order not found.");
            }

            return Ok(new { Message = "Order updated successfully", Order = updatedOrder });
        }

      
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);

            if (order == null)
            {
                return NotFound("Order not found."); 
            }

            return Ok(order);
        }

        // Get Orders by User ID
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(int userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId.ToString());

            if (orders == null || !orders.Any())
            {
                return NotFound("No orders found for this user.");
            }

            return Ok(orders);
        }

        // Delete Order
        [HttpDelete("delete/{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            var isDeleted = await _orderRepository.DeleteOrderAsync(orderId);

            if (!isDeleted)
            {
                return NotFound("Order not found.");
            }

            return Ok(new { Message = "Order deleted successfully" });
        }
    }
}
