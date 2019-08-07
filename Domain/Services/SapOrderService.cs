using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Domain.Output;
using GRINTSYS.SAPRestApi.Domain.Services;
using GRINTSYS.SAPRestApi.Inputs;
using SAPbobsCOM;
using System;
using System.Threading.Tasks;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public class SapOrder: SapDocumentServiceBase, ISapDocumentService
    {
        private IOrderService _orderService;
        public SapOrder(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public override async Task<TaskResponse> Execute(ISapDocumentInput input)
        {
            String message = "";
            var order = await _orderService.GetAsync(((SAPOrderInput)input).Id);

            Company company = this.Connect(new SapSettingsInput());

            IDocuments salesOrder = (IDocuments)company.GetBusinessObject(BoObjectTypes.oOrders);
            salesOrder.CardCode = order.CardCode;
            salesOrder.Comments = order.Comment;
            salesOrder.Series = order.Series;
            salesOrder.SalesPersonCode = order.AbpUser.SalesPersonId;
            salesOrder.DocDueDate = order.CreationTime;      

            foreach (var item in order.OrderItems)
            {
                salesOrder.Lines.ItemCode = item.Code;
                salesOrder.Lines.Quantity = item.Quantity;
                salesOrder.Lines.TaxCode = item.TaxCode;
                salesOrder.Lines.DiscountPercent = item.DiscountPercent;
                salesOrder.Lines.WarehouseCode = item.WarehouseCode;
                //Add Comercial Canal, Tradicion Center Cost. DEM. July 8th. 2018. 
                salesOrder.Lines.CostingCode = "301";
                salesOrder.Lines.CostingCode2 = "3001-01";
                salesOrder.Lines.Add();
            }
            // add Sales Order
            if (salesOrder.Add() == 0)
            {
                message = String.Format("Successfully added Sales Order DocEntry: {0}", company.GetNewObjectKey());
                //Logger.Info(message);             
            }
            else
            {
                message = "Error Code: "
                        + company.GetLastErrorCode().ToString()
                        + " - "
                        + company.GetLastErrorDescription();
                //Logger.Error(message);
            }

            //order.LastMessage = message;
            //_orderManager.UpdateOrder(order);

            company.Disconnect();

            return new TaskResponse() { Message = message };
        }
    }
}
