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
    public class PurchaseOrderRepository : BaseRepository, IPurchaseOrderRepository
    {
        public PurchaseOrderRepository(SAPRestApiContext context) : base(context)
        {
        }
        public async Task<PurchaseOrder> GetAsync(int id)
        {
            return await _context.PurchaseOrders
                                 .Include("PurchaseOrderDetails")
                                 .Include("PurchaseOrderExpenses")
                                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task UpdateAsync(PurchaseOrder purchaseOrder)
        {
            _context.Entry<PurchaseOrder>(purchaseOrder);
            return _context.SaveChangesAsync();
        }
    }
}