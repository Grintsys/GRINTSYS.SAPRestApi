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
    public class ClientRepository : BaseRepository, IClientRepository
    {
        public ClientRepository(SAPRestApiContext context) : base(context)
        {
        }

        public async Task<Client> GetByCardCodeAsync(string cardCode)
        {
            return await _context.Clients
               .FirstOrDefaultAsync(x => x.CardCode == cardCode);
        }
    }
}