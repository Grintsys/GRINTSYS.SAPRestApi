using GRINTSYS.SAPRestApi.Models;
using GRINTSYS.SAPRestApi.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IPurchaseOrderRepository _purchaseOrderRepository;
        public PurchaseOrderService(IPurchaseOrderRepository purchaseOrderRepository) : base()
        {
            _purchaseOrderRepository = purchaseOrderRepository;
        }
        public async Task<PurchaseOrder> GetAsync(int id)
        {
            return await _purchaseOrderRepository.GetAsync(id);
        }

        public Task UpdateAsync(PurchaseOrder purchaseOrder)
        {
            return _purchaseOrderRepository.UpdateAsync(purchaseOrder);
        }
    }
}