using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Domain.Output;
using GRINTSYS.SAPRestApi.Inputs;
using GRINTSYS.SAPRestApi.Models;
using SAPbobsCOM;
using System;
using System.Threading.Tasks;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    
    public class SapPayment: SapDocumentServiceBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IInvoiceService _invoiceService;

        public SapPayment(IPaymentService paymentService, 
            IInvoiceService invoiceService)
        {
            _paymentService = paymentService;
            _invoiceService = invoiceService;
        }

        public override async Task<TaskResponse> Execute(ISapDocumentInput input)
        {
            var response = new TaskResponse();
            var payment = await _paymentService.GetAsync(((SAPPaymentInput)input).Id);
            var invoice = await _invoiceService.GetAsync(payment.InvoiceId);

            Company company = this.Connect(new SapSettingsInput());

            Payments paymentDoc = (Payments)company.GetBusinessObject(BoObjectTypes.oIncomingPayments);
            paymentDoc.DocObjectCode = BoPaymentsObjectType.bopot_IncomingPayments;

            paymentDoc.DocTypte = BoRcptTypes.rCustomer;
            paymentDoc.CardCode = invoice.Client.CardCode;
            paymentDoc.DocDate = payment.CreationTime;
            paymentDoc.VatDate = payment.CreationTime;
            paymentDoc.DueDate = payment.CreationTime;
            paymentDoc.Remarks = payment.Comment;
            paymentDoc.CounterReference = payment.ReferenceNumber;
            if(paymentDoc.UserFields.Fields.Count > 0)
                paymentDoc.UserFields.Fields.Item("U_Cobrador").Value = payment.AbpUser.CollectId;

            if(payment.Type == (int)PaymentType.Efectivo)
            {
                paymentDoc.CashAccount = payment.Bank.GeneralAccount;
                paymentDoc.CashSum = payment.PayedAmount;
            }

            if (payment.Type == (int)PaymentType.Transferencia)
            {
                paymentDoc.TransferAccount = payment.Bank.GeneralAccount;
                paymentDoc.TransferDate = payment.PayedDate;
                paymentDoc.TransferReference = payment.ReferenceNumber;
                paymentDoc.TransferSum = payment.PayedAmount;
            }

            if(payment.Type == (int)PaymentType.Cheque)
            {
                paymentDoc.Checks.CheckAccount = payment.Bank.GeneralAccount;
                paymentDoc.Checks.CheckSum = payment.PayedAmount;
                paymentDoc.Checks.DueDate = payment.PayedDate;
                paymentDoc.Checks.BankCode = payment.Bank.Code;
                paymentDoc.Checks.Add();
            }

            paymentDoc.Invoices.DocEntry = invoice.DocEntry;
            paymentDoc.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
            paymentDoc.Invoices.SumApplied = payment.PayedAmount;
            paymentDoc.Invoices.Add();

            int errorCode = paymentDoc.Add();

            if (errorCode != 0)
            {
                var errorMessage = "Error Code: "
                            + company.GetLastErrorCode().ToString()
                            + " - "
                            + company.GetLastErrorDescription();

                payment.LastMessage = errorMessage;
                // _paymentManager.UpdatePayment(payment);
                // Logger.Error(errorMessage);
                response.Message = errorMessage;
                response.Success = false;
                throw new Exception(errorMessage);
            }
            else
            {
                String key = company.GetNewObjectKey();
                payment.DocEntry = key;
                payment.Status = String.IsNullOrEmpty(key) ? (int)PaymentStatus.Error : (int)PaymentStatus.Autorizado;
                var message = String.Format("Successfully Autorized Payment DocEntry: {0}", key);
                payment.LastMessage = message;
                response.Message = message;
                response.Success = false;
                //Logger.Info(message);
                //_paymentManager.UpdatePayment(payment);
            }

            company.Disconnect();

            return response;
        }
    }
}
