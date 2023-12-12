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
    }
}