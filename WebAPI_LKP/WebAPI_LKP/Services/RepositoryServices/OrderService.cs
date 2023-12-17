using MailKit.Search;
using WebAPI_LKP.DTO;
using WebAPI_LKP.Interfaces.Repositories;
using WebAPI_LKP.Interfaces.Services;
using WebAPI_LKP.Models;

namespace WebAPI_LKP.Services.RepositoryServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(
            IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<bool> CreateOrder(List<OrderDTO> orderDtoList, User user)
        {
            // Create a list of Order entities using LINQ
            var orders = orderDtoList.Select(orderDto => new Order
            {
                ProductId = orderDto.ProductId,
                Delivery = orderDto.Delivery,
                Quantity = orderDto.Quantity,
                UserId = user.Id
            }).ToList();

            return await _orderRepository.AddOrders(orders);
        }

        public async Task<List<OrderDTO>> GetUserOrders(Guid userId)
        {
            var orderList = await _orderRepository.GetOrdersById(userId);
            var orderDtos = orderList.Select(orders => new OrderDTO
            {
                Delivery = orders.Delivery,
                Quantity = orders.Quantity,
                ProductId = orders.ProductId,
            }).ToList();

            return orderDtos;
        }

        public async Task<OrderDTO> GetOrder(Guid orderId)
        {
            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null)
            {
                return null;
            }

            var orderDto = new OrderDTO
            {
                Delivery = order.Delivery,
                Quantity = order.Quantity,
                ProductId = order.ProductId,
            };

            return orderDto;
        }

        public async Task<bool> UpdateOrder(OrderDTO updateOrder, User user)
        {
            var order = new Order()
            {
                Id = updateOrder.OrderId,
                Delivery = updateOrder.Delivery,
                ProductId = updateOrder.ProductId,
                Quantity = updateOrder.Quantity,
                UserId = user.Id,
            };

            return await _orderRepository.UpdateOrder(order);
        }

        public async Task<bool> DeleteOrder(Guid orderId)
        {
            return await _orderRepository.DeleteOrder(orderId);
        }
    }
}