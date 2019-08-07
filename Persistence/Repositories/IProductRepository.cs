using GRINTSYS.SAPRestApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRINTSYS.SAPRestApi.Persistence.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetAsync(int id);
    }
}