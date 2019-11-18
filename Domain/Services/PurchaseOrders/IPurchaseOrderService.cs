using GRINTSYS.SAPRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public interface IPurchaseOrderService
    {
        Task<PurchaseOrder> GetAsync(int id);
        Task UpdateAsync(PurchaseOrder purchaseOrder);
    }
}