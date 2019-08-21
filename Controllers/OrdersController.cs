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
    public class OrdersController : ApiController
    {
        private readonly ISapDocumentService _sapDocumentService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public OrdersController(SapOrder sapDocumentService)
        {
            _sapDocumentService = sapDocumentService;
        }

        // [GET] api/orders/1
        [ResponseType(typeof(TaskResponse))]
        public async Task<IHttpActionResult> Get(int id)
        {
            log.Info("[GET] api/orders/1");

            var jobId = BackgroundJob.Enqueue(
                () => this.CreateSalesOrderOnSap(id));

            var result = new TaskResponse()
            {
                Success = true,
                Message = String.Format("Ejecutandose JobId:{0}", jobId)
            };

            return Ok(result);
        }

        [AutomaticRetry(Attempts = 0)]
        public void CreateSalesOrderOnSap(int orderId)
        {
             _sapDocumentService.Execute(new SAPOrderInput() { Id = orderId });
        }
    }
}
