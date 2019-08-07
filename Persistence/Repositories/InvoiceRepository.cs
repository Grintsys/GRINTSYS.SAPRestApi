using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GRINTSYS.SAPRestApi.Models;
using GRINTSYS.SAPRestApi.Persistance.Repositories;

namespace GRINTSYS.SAPRestApi.Persistence.Repositories
{
    public class InvoiceRepository : BaseRepository, IInvoiceRepository
    {
        public InvoiceRepository(SAPRestApiContext context) : base(context)
        {
        }

        public async Task<Invoice> GetAsync(int id)
        {
            return await _context.Invoices.Include("Client").FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}