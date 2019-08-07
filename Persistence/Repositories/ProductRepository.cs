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
    public class ProductRepository : BaseRepository, IProductRepository
    {
        public ProductRepository(SAPRestApiContext context) : base(context)
        {
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _context.Products.Include("ProductVariants")
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}