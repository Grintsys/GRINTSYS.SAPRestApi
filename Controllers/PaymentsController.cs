using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Domain.Output;
using GRINTSYS.SAPRestApi.Domain.Services;
using GRINTSYS.SAPRestApi.Models;
using Hangfire;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace GRINTSYS.SAPRestApi.Controllers
{
    public class PaymentsController : ApiController
    {
        private readonly ISapDocumentService _sapDocumentService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public PaymentsController(SapPayment sapDocumentService)
        {
            _sapDocumentService = sapDocumentService;
        }

        [ResponseType(typeof(TaskResponse))]
        public async Task<IHttpActionResult> Get(int id)
        {
            log.Info(String.Format("[GET] api/payments/{0}", id));

            var jobId = BackgroundJob.Enqueue(
                () => this.CreatePaymentOnSap(id));

            var result = new TaskResponse()
            {
                Success = true,
                Message = String.Format("Ejecutandose JobId:{0}", jobId)
            };

            return Ok(result);
        }

        [AutomaticRetry(Attempts = 0)]
        public void CreatePaymentOnSap(int paymentId)
        {
            _sapDocumentService.Execute(new SAPPaymentInput() { Id = paymentId });
        }
    }
}
