using GRINTSYS.SAPRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GRINTSYS.SAPRestApi.Persistence.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetAsync(int id);
        Task UpdateAsync(Payment payment);
    }
}