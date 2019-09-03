using GRINTSYS.SAPRestApi.Models;
using System.Threading.Tasks;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public interface IProductService
    {
        Task<Product> GetAsync(int id);
    }
}