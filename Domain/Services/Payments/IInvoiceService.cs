using GRINTSYS.SAPRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public interface IInvoiceService
    {
        Task<Invoice> GetAsync(int id);
    }
}