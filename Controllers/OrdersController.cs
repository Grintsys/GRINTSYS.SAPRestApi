using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Domain.Output;
using GRINTSYS.SAPRestApi.Domain.Services;
using GRINTSYS.SAPRestApi.Domain.Services.Input;
using GRINTSYS.SAPRestApi.Models;
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
        private readonly OrderService _orderService;
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly Expression<Func<Order, OrderOuput>> AsOrderDto =
           x => new OrderOuput
           {
               Id = x.Id,
               CardCode = x.CardCode,
               Comment= x.Comment,
               LastMessage = x.LastMessage,
               Status = x.Status,
               UserId = x.UserId,
               CreationTime = x.CreationTime
           };

        public OrdersController(SapOrder sapDocumentService, 
            OrderService orderService)
        {
            _sapDocumentService = sapDocumentService;
            _orderService = orderService;
        }

        // GET api/orders
        [ResponseType(typeof(TaskResponse))]
        //[AcceptVerbs("POST,GET")]
        [HttpPost]
        //[HttpGet]
        //[Route("orders/create")]
        public async Task<IHttpActionResult> Post([FromBody] OrderInput input)
        {
            log.Info("[POST] /api/orders");
            var result = await _sapDocumentService.Execute(new SAPOrderInput() { Id = input.Id});

            return Ok(result);
        }

        // [GET] api/orders/1
        [ResponseType(typeof(TaskResponse))]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            /*
            log.Info("[GET] /api/orders/" + id);
            var x = await _orderService.GetAsync(id);

            if( x == null)
            {
                return NotFound();
            }

            return Ok(new OrderOuput
            {
                Id = x.Id,
                CardCode = x.CardCode,
                Comment = x.Comment,
                LastMessage = x.LastMessage,
                Status = x.Status,
                UserId = x.UserId,
                CreationTime = x.CreationTime
            });
            */

            log.Info("[POST] api/orders/1");
            var result = await _sapDocumentService.Execute(new SAPOrderInput() { Id = id });

            return Ok(result);
        }
    }
}
