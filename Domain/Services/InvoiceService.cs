using GRINTSYS.SAPRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public class InvoiceService: IInvoiceService
    {
        private readonly IInvoiceService _invoiceRepository;
        public InvoiceService(IInvoiceService invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<Invoice> GetAsync(int id)
        {
            return await _invoiceRepository.GetAsync(id);
        }
    }
}