using GRINTSYS.SAPRestApi.BussinessLogic.Inputs;
using GRINTSYS.SAPRestApi.Domain.Output;
using GRINTSYS.SAPRestApi.Inputs;
using GRINTSYS.SAPRestApi.Models;
using GRINTSYS.SAPRestApi.Persistence.Repositories;
using SAPbobsCOM;
using System;
using System.Threading.Tasks;

namespace GRINTSYS.SAPRestApi.Domain.Services
{
    public enum PaymentStatus
    {
        CreadoEnAplicacion = 0,
        CreadoEnSAP = 1,
        Error = 2,
        CanceladoPorFinanzas = 3,
        Autorizado = 4
    }

    public enum PaymentType
    {
        Efectivo = 0,
        Cheque = 1,
        Transferencia = 2
    }

    public class SapPayment : SapDocumentServiceBase
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IPaymentService _paymentService;
        private ITenantRepository _tenantRepository;

        public SapPayment(IPaymentService paymentService,
            ITenantRepository tenantRepository) : base()
        {
            _paymentService = paymentService;
            _tenantRepository = tenantRepository;
        }

        public override async Task<TaskResponse> Execute(ISapDocumentInput input)
        {
            log.Info($"Begin to create a payment {((SAPPaymentInput)input).Id}");
            TaskResponse response = new TaskResponse() { Success = true, Message = string.Empty };
            try
            {
                var payment = await _paymentService.GetAsync(((SAPPaymentInput)input).Id);
                var tenant = await _tenantRepository.GetTenantById(payment.TenantId);

                Company company = this.Connect(new SapSettingsInput { Companydb = tenant.SAPDatabase });

                Payments paymentDoc = (Payments)company.GetBusinessObject(BoObjectTypes.oIncomingPayments);
                paymentDoc.DocObjectCode = BoPaymentsObjectType.bopot_IncomingPayments;

                paymentDoc.DocTypte = BoRcptTypes.rCustomer;
                paymentDoc.CardCode = payment.CardCode;
                paymentDoc.DocDate = payment.PayedDate;
                paymentDoc.VatDate = payment.PayedDate;
                paymentDoc.DueDate = payment.PayedDate;
                paymentDoc.Remarks = payment.Comment;
                paymentDoc.CounterReference = payment.ReferenceNumber;
                if (paymentDoc.UserFields.Fields.Count > 0)
                {
                    paymentDoc.UserFields.Fields.Item("U_Cobrador").Value = "C79";//payment.AbpUser.CollectId;
                }

                if (payment.Type == (int)PaymentType.Efectivo)
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

                if (payment.Type == (int)PaymentType.Cheque)
                {
                    paymentDoc.Checks.CheckAccount = payment.Bank.GeneralAccount;
                    paymentDoc.Checks.CheckSum = payment.PayedAmount;
                    paymentDoc.Checks.DueDate = payment.PayedDate;
                    paymentDoc.Checks.BankCode = payment.Bank.Code;
                    paymentDoc.Checks.Add();
                }

                if (payment.paymentInvoiceItems != null)
                {
                    foreach (var invoice in payment.paymentInvoiceItems)
                    {
                        paymentDoc.Invoices.DocEntry = invoice.DocEntry;
                        paymentDoc.Invoices.InvoiceType = BoRcptInvTypes.it_Invoice;
                        paymentDoc.Invoices.SumApplied = invoice.PayedAmount;
                        paymentDoc.Invoices.Add();
                    }
                }

                string message;
                if (paymentDoc.Add() == 0)
                {
                    string newObjectKey = company.GetNewObjectKey();
                    message = $"Successfully added Payment. DocEntry: {newObjectKey}";
                    payment.DocEntry = newObjectKey;
                    payment.Status = (int)PaymentStatus.CreadoEnSAP;
                    log.Info(message);
                }
                else
                {
                    message = $"Error Code: {company.GetLastErrorCode().ToString()} - {company.GetLastErrorDescription()}";
                    payment.Status = (int)PaymentStatus.Error;
                    response.Success = false;
                    log.Error(message);
                }
                System.Runtime.InteropServices.Marshal.ReleaseComObject(paymentDoc);
                paymentDoc = null;
                company.Disconnect();

                response.Message = message;
                payment.LastMessage = message;

                await _paymentService.UpdateAsync(payment);
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
