using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Domain.Output;
using GRINTSYS.SAPRestApi.Domain.Services;
using GRINTSYS.SAPRestApi.Domain.Services.Input;
using GRINTSYS.SAPRestApi.Models;
using Hangfire;
using System;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace GRINTSYS.SAPRestApi.Controllers
{
    public class PurchaseOrdersController : ApiController
    {
        private readonly ISapDocumentService _sapDocumentService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PurchaseOrdersController()
        {

        }
        public PurchaseOrdersController(SapPurchaseOrder sapDocumentService)
        {
            _sapDocumentService = sapDocumentService;
        }

        // [GET] api/purchaseorders/1
        [ResponseType(typeof(TaskResponse))]
        public async Task<IHttpActionResult> Get(int id)
        {
            log.Info($"[GET] api/purchaseorders/{id}");

            var jobId = BackgroundJob.Enqueue(
                () => this.CreatePurchaseOrderToSap(id));

            var result = new TaskResponse()
            {
                Success = true,
                Message = $"Ejecutandose JobId:{jobId}"
            };

            return Ok(result);
        }

        [AutomaticRetry(Attempts = 0)]
        [Queue("purchaseorder_gt")]
        public void CreatePurchaseOrderToSap(int orderId)
        {
            _sapDocumentService.Execute(new SAPOrderInput() { Id = orderId });
        }
    }
}