using GRINTSYS.SAPRestApi.Domain.Services;
using GRINTSYS.SAPRestApi.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GRINTSYS.SAPRestApi.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET api/products/5
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            var order = await _orderService.GetAsync(id);       
        
            return Request.CreateResponse(HttpStatusCode.OK, order, Configuration.Formatters.JsonFormatter);
        }
    }
}
