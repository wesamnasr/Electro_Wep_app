using Microsoft.EntityFrameworkCore;
using MY_API_PROJECT.DTO.OrderItemDTOs;
using MY_API_PROJECT.DTO.OrderDTOs;
using MY_API_PROJECT.Interfaces;
using MY_API_PROJECT.Models;

namespace MY_API_PROJECT.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDBContext _context;

        public OrderRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Order?> CreateOrderAsync(OrderCreateDTO orderDto)
        {
        
            var products = await _context.Products
                .Where(p => orderDto.ProductIDs.Contains(p.ID))
                .ToListAsync();

            if (products.Count != orderDto.ProductIDs.Count)
                return null; 

           
            var order = new Order
            {
                UserID = orderDto.UserID,
                TotalAmount = orderDto.TotalAmount,
                Status = orderDto.OrderStatus,
                OrderDate = orderDto.OrderDate,
                OrderItems = products.Select(p => new OrderItem
                {
                    ProductID = p.ID,
                    Quantity = 1, 
                    UnitPrice = p.Price 
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<bool> DeleteOrderAsync(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null) return false;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Select(o => new OrderDTO
                {
                    ID = o.ID,
                    UserID = o.UserID,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        ProductID = oi.ProductID,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<OrderDTO?> GetOrderByIdAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.ID == orderId);

            if (order == null) return null;

            return new OrderDTO
            {
                ID = order.ID,
                UserID = order.UserID,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    ProductID = oi.ProductID,
                    Quantity = oi.Quantity,
                    UnitPrice = oi.UnitPrice
                }).ToList()
            };
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserID == userId)
                .Select(o => new OrderDTO
                {
                    ID = o.ID,
                    UserID = o.UserID,
                    TotalAmount = o.TotalAmount,
                    Status = o.Status,
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems.Select(oi => new OrderItemDTO
                    {
                        ProductID = oi.ProductID,
                        Quantity = oi.Quantity,
                        UnitPrice = oi.UnitPrice
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<Order?> UpdateOrderAsync(int orderId, OrderUpdateDTO orderUpdateDTO)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems) 
                .FirstOrDefaultAsync(o => o.ID == orderId);

            if (order == null)
            {
                return null; 
            }

            
            order.Status = orderUpdateDTO.Status;
            order.TotalAmount = orderUpdateDTO.TotalAmount;

        
            foreach (var item in orderUpdateDTO.OrderItems)
            {
                var existingItem = order.OrderItems.FirstOrDefault(oi => oi.ProductID == item.ProductID);
                if (existingItem != null)
                {
                    
                    existingItem.Quantity = item.Quantity;
                    existingItem.UnitPrice = item.UnitPrice;
                }
                else
                {
                    order.OrderItems.Add(new OrderItem
                    {
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        OrderID = order.ID
                    });
                }
            }

            await _context.SaveChangesAsync();

            return order;
        }

    }
}
