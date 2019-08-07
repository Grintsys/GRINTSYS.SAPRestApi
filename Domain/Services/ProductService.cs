using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using GRINTSYS.SAPRestApi.Models;
using GRINTSYS.SAPRestApi.Persistence.Repositories;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _productRepository.GetAsync(id);
        }
    }
}