using GRINTSYS.SAPRestApi.Models;
using GRINTSYS.SAPRestApi.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GRINTSYS.SAPRestApi.Persistence.Repositories
{
    public class OrderRepository : BaseRepository, IOrderRepository
    {
        public OrderRepository(SAPRestApiContext context) : base(context)
        {
        }

        public async Task<Order> GetAsync(int id)
        {
            return await _context.Orders.Include("OrderItems")
               .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(Order order)
        {
            _context.Entry<Order>(order);
            return _context.SaveChangesAsync();
        }
    }
}