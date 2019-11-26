using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Domain.Output;
using GRINTSYS.SAPRestApi.Domain.Services;
using GRINTSYS.SAPRestApi.Inputs;
using GRINTSYS.SAPRestApi.Persistence.Repositories;
using SAPbobsCOM;
using System;
using System.Threading.Tasks;

namespace GRINTSYS.SAPRestApi.Domain.Services
{

    public class SapPurchaseOrder : SapDocumentServiceBase, ISapDocumentService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IPurchaseOrderService _purchaseOrderService;

        public SapPurchaseOrder(IPurchaseOrderService purchaseOrderService) : base()
        {
            _purchaseOrderService = purchaseOrderService;
        }

        public override async Task<TaskResponse> Execute(ISapDocumentInput input)
        {
            log.Info($"Begin to create a purchase order {((SAPOrderInput)input).Id}");

            TaskResponse response = new TaskResponse() { Success = true, Message = string.Empty };
            string message = string.Empty;

            try
            {
                var purchaseOrder = await _purchaseOrderService.GetAsync(((SAPOrderInput)input).Id);
                
                Company company = this.Connect(new SapSettingsInput { Companydb = "SBO_KGT_PRUEBAS"/*tenant.SAPDatabase*/ });

                IDocuments sapPurchaseOrder = (IDocuments)company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                sapPurchaseOrder.DocType = BoDocumentTypes.dDocument_Items;
                sapPurchaseOrder.DocDate = DateTime.Now;//purchaseOrder.DocDate;
                sapPurchaseOrder.DocDueDate = DateTime.Now;
                sapPurchaseOrder.CardCode = "P000001";//purchaseOrder.CardCode;
                sapPurchaseOrder.CardName = "VAN HEUSEN DE CENTRO AMERICA, S.de R.L. de C.V.";//purchaseOrder.CardName;
                //sapPurchaseOrder.DocTotal = Convert.ToDouble(purchaseOrder.DocTotal);

                sapPurchaseOrder.DocCurrency = purchaseOrder.DocCurrency;
                sapPurchaseOrder.Comments = purchaseOrder.Comments;
                sapPurchaseOrder.SalesPersonCode = purchaseOrder.SlpCode;
                //sapPurchaseOrder.Series = 0;
                //sapPurchaseOrder.NumAtCard = string.Empty;

                if (sapPurchaseOrder.UserFields.Fields.Count > 0)
                {
                    sapPurchaseOrder.UserFields.Fields.Item("U_FacNom").Value = "VAN HEUSEN DE CENTRO AMERICA, S.de R.L. de C.V.";
                    sapPurchaseOrder.UserFields.Fields.Item("U_Tipo_Facturacion").Value = "M";
                    sapPurchaseOrder.UserFields.Fields.Item("U_FacNit").Value = string.Empty;
                    sapPurchaseOrder.UserFields.Fields.Item("U_FacFecha").Value = purchaseOrder.DocDate.ToShortDateString();
                    sapPurchaseOrder.UserFields.Fields.Item("U_TIPO_DOCUMENTO").Value = "FC";
                    sapPurchaseOrder.UserFields.Fields.Item("U_M2_UUID").Value = purchaseOrder.U_M2_UUID.ToString();
                }

                foreach (var item in purchaseOrder.PurchaseOrderDetails)
                {
                    sapPurchaseOrder.Lines.ItemCode = item.ItemCode;
                    //sapPurchaseOrder.Lines.ItemDescription = item.Dscription;
                    sapPurchaseOrder.Lines.Quantity = item.Quantity;
                    sapPurchaseOrder.Lines.Price = Convert.ToDouble(item.Price);
                    sapPurchaseOrder.Lines.Currency = item.LineCurrency;
                    sapPurchaseOrder.Lines.TaxCode = item.TaxCode;
                    //sapPurchaseOrder.Lines.LineTotal = Convert.ToDouble(item.LineTotal);

                    //Settigs By Tenant
                    //sapPurchaseOrder.Lines.CostingCode = "303"/*tenant.CostingCode*/;
                    //sapPurchaseOrder.Lines.CostingCode2 = "3003-01"/*tenant.CostingCode2*/;
                    //sapPurchaseOrder.Lines.CostingCode3 = string.Empty;
                    //sapPurchaseOrder.Lines.AccountCode = string.Empty;
                    sapPurchaseOrder.Lines.Add();
                }

                /*foreach (var item in purchaseOrder.PurchaseOrderExpenses)
                {
                    sapPurchaseOrder.Expenses.ExpenseCode = item.ExpnsCode;
                    sapPurchaseOrder.Expenses.TaxCode = item.TaxCode;
                    sapPurchaseOrder.Expenses.DistributionMethod = BoAdEpnsDistribMethods.aedm_None;
                    sapPurchaseOrder.Expenses.LineTotal = Convert.ToDouble(item.LineTotal);
                    //sapPurchaseOrder.Expenses.UserFields.Fields.Item("U_TipoA").Value = item.U_TipoA;
                    
                    sapPurchaseOrder.Expenses.Add();
                }*/                

                if (sapPurchaseOrder.Add() == 0)
                {
                    message = $"Successfully added Purchase Order. DocEntry: {company.GetNewObjectKey()}";
                    purchaseOrder.Status = 2;
                    purchaseOrder.RemoteId = Convert.ToInt32(company.GetNewObjectKey());
                    log.Info(message);
                }
                else
                {
                    message = $"Error Code: {company.GetLastErrorCode().ToString()} - {company.GetLastErrorDescription()}";
                    purchaseOrder.Status = 3;
                    response.Success = false;
                    log.Error(message);
                }

                response.Message = message;
                purchaseOrder.LastMessage = message;

                await _purchaseOrderService.UpdateAsync(purchaseOrder);

                ////recomended from http://www.appseconnect.com/di-api-memory-leak-in-sap-business-one-9-0/
                System.Runtime.InteropServices.Marshal.ReleaseComObject(purchaseOrder);
                purchaseOrder = null;
                company.Disconnect();
            }
            catch (Exception e)
            {
                response.Success = false;
                response.Message = e.Message;
                log.Error(e.Message);
            }

            return response;
        }
    }
}