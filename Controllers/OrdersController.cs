using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
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
        private readonly ISapDocumentService _sapDocumentService;

        public OrdersController(SapOrder sapDocumentService)
        {
            _sapDocumentService = sapDocumentService;
        }

        // GET api/orders/5
        [HttpGet]
        [HttpPost]
        public async Task<HttpResponseMessage> Create(int id)
        {
            var result = await _sapDocumentService.Execute(new SAPOrderInput() { Id = id });

            return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }
    }
}
