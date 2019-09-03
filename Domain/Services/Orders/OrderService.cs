using GRINTSYS.SAPRestApi.Models;
using GRINTSYS.SAPRestApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository):base()
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order> GetAsync(int id)
        {
            return await _orderRepository.GetAsync(id);
        }

        public Task UpdateAsync(Order order)
        {
            return _orderRepository.UpdateAsync(order);
        }
    }
}