using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Domain.Output;
using GRINTSYS.SAPRestApi.Domain.Services;
using GRINTSYS.SAPRestApi.Inputs;
using SAPbobsCOM;
using System;
using System.Threading.Tasks;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public enum OrderStatus
    {
        CreadoEnAplicacion = 0,
        PreliminarEnSAP = 1,
        Autorizado = 2,
        ErrorAlCrearEnSAP = 3
    }

    public class SapOrder: SapDocumentServiceBase, ISapDocumentService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IOrderService _orderService;
        private IClientService _clientService;
        public SapOrder(IOrderService orderService, IClientService clientService)
        {
            _orderService = orderService;
            _clientService = clientService;
        }

        public override async Task<TaskResponse> Execute(ISapDocumentInput input)
        {
            log.Info("Begin to create a order");

            TaskResponse response = new TaskResponse() { Success = true, Message = "" };
            String message = "";
            try
            {
                var order = await _orderService.GetAsync(((SAPOrderInput)input).Id);

                Company company = this.Connect(new SapSettingsInput());

                IDocuments salesOrder = (IDocuments)company.GetBusinessObject(BoObjectTypes.oOrders);
                salesOrder.CardCode = order.CardCode;
                salesOrder.Comments = order.Comment;
                salesOrder.Series = order.Series;
                salesOrder.SalesPersonCode = order.AbpUser.SalesPersonId;
                salesOrder.DocDueDate = order.CreationTime;

                //fix for M2 dimension
                var client = await _clientService.GetByCardCodeAsync(order.CardCode);

                foreach (var item in order.OrderItems)
                {
                    salesOrder.Lines.ItemCode = item.Code;
                    salesOrder.Lines.Quantity = item.Quantity;
                    salesOrder.Lines.TaxCode = item.TaxCode;
                    salesOrder.Lines.DiscountPercent = item.DiscountPercent;
                    salesOrder.Lines.WarehouseCode = item.WarehouseCode;
                    //Add Comercial Canal, Tradicion Center Cost. DEM. July 8th. 2018. 
                    //Fixed by tenant
                    salesOrder.Lines.CostingCode = "303";
                    salesOrder.Lines.CostingCode2 = "3001-01";
                    salesOrder.Lines.CostingCode3 = client.Dimension;
                    salesOrder.Lines.Add();
                }
                // add Sales Order
                if (salesOrder.Add() == 0)
                {
                    message = String.Format("Successfully added Sales Order DocEntry: {0}", company.GetNewObjectKey());
                    //Logger.Info(message); 
                    log.Info(message);
                }
                else
                {
                    message = "Error Code: "
                            + company.GetLastErrorCode().ToString()
                            + " - "
                            + company.GetLastErrorDescription();

                    response.Success = false;
                    response.Message = message;
                    log.Error(message);
                }

                order.LastMessage = message;
                order.Status = (int)OrderStatus.PreliminarEnSAP;
                await _orderService.UpdateAsync(order);

                company.Disconnect();
            }catch(Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                log.Error(e.Message);
            }

            return response;
        }
    }
}
