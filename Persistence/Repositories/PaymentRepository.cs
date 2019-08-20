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
    public class PaymentRepository : BaseRepository, IPaymentRepository
    {

        public PaymentRepository(SAPRestApiContext context) : base(context)
        {
        }

        public async Task<Payment> GetAsync(int id)
        {
            return await _context.Payments.Include("AbpUser")
                .Include("Bank")
                .Include("PaymentInvoiceItem")
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}