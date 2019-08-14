using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Domain.Services;
using GRINTSYS.SAPRestApi.Models;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GRINTSYS.SAPRestApi.Controllers
{
    public class PaymentsController : ApiController
    {
        private readonly ISapDocumentService _sapDocumentService;


        public PaymentsController(SapPayment sapDocumentService)
        {
            _sapDocumentService = sapDocumentService;
        }

        // GET api/products/5
        [HttpPost]
        public async Task<HttpResponseMessage> Create(int id)
        {
            var result = await _sapDocumentService.Execute(new SAPOrderInput() { Id = id });

            return Request.CreateResponse(HttpStatusCode.OK, result, Configuration.Formatters.JsonFormatter);
        }
    }
}
