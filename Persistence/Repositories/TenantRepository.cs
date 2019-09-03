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
    public class TenantRepository : BaseRepository, ITenantRepository
    {
        public TenantRepository(SAPRestApiContext context) : base(context)
        {
        }

        public async Task<AbpTenant> GetTenantById(int tenantId)
        {
            return await _context.AbpTenants
              .FirstOrDefaultAsync(x => x.Id == tenantId);
        }
    }
}