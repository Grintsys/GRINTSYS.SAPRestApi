using GRINTSYS.SAPRestApi.Models;
using GRINTSYS.SAPRestApi.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GRINTSYS.SAPRestApi.Persistence.Repositories
{
    public interface IPurchaseOrderRepository
    {
        Task<PurchaseOrder> GetAsync(int id);
        Task UpdateAsync(PurchaseOrder purchaseOrder);
    }
}