using GRINTSYS.SAPRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPRestApi.Persistence.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetByCardCodeAsync(string cardCode);
    }
}
