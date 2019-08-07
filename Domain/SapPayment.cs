using Abp.Application.Services;
using Abp.UI;
using GRINTSYS.SAPMiddleware.M2;
using GRINTSYS.SAPMiddleware.M2.Payments;
using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GRINTSYS.SAPRestApi.BussinessLogic
{
    public class SapPayment: SapDocument
    {
        private readonly PaymentManager _paymentManager;

        public SapPayment(PaymentManager paymentManager)
        {
            _paymentManager = paymentManager;
        }

        public override void Execute(SapDocumentInput input)
        {
            var payment = _paymentManager.GetPayment(input.Id);
            var invoice = _paymentManager.GetInvoice(payment.InvoiceId);

            Company company = this.Connect(new SapInput());

            SAPbobsCOM.Payments paymentDoc = (SAPbobsCOM.Payments)company.GetBusinessObject(BoObjectTypes.oIncomingPayments);
            paymentDoc.DocObjectCode = BoPaymentsObjectType.bopot_IncomingPayments;

            paymentDoc.DocTypte = BoRcptTypes.rCustomer;
            paymentDoc.CardCode = invoice.Client.CardCode;
            paymentDoc.DocDate = payment.CreationTime;
            paymentDoc.VatDate = payment.CreationTime;
            paymentDoc.DueDate = payment.CreationTime;
            paymentDoc.Remarks = payment.Comment;
            paymentDoc.CounterReference = payment.ReferenceNumber;
            if(paymentDoc.UserFields.Fields.Count > 0)
                paymentDoc.UserFields.Fields.Item("U_Cobrador").Value = payment.User.CollectId;

            if(payment.Type == PaymentType.Efectivo)
            {
                paymentDoc.CashAccount = payment.Bank.GeneralAccount;
                paymentDoc.CashSum = payment.PayedAmount;
            }

            if (payment.Type == PaymentType.Transferencia)
            {
                paymentDoc.TransferAccount = payment.Bank.GeneralAccount;
                paymentDoc.TransferDate = payment.PayedDate;
                paymentDoc.TransferReference = payment.ReferenceNumber;
                paymentDoc.TransferSum = payment.PayedAmount;
            }

            if(payment.Type == PaymentType.Cheque)
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
                _paymentManager.UpdatePayment(payment);
                Logger.Error(errorMessage);
                throw new UserFriendlyException(errorMessage);
            }
            else
            {
                String key = company.GetNewObjectKey();
                payment.DocEntry = key;
                payment.Status = String.IsNullOrEmpty(key) ? PaymentStatus.Error : PaymentStatus.Autorizado;
                var message = String.Format("Successfully Autorized Payment DocEntry: {0}", key);
                payment.LastMessage = message;
                Logger.Info(message);
                _paymentManager.UpdatePayment(payment);
            }

            company.Disconnect();
        }
    }
}
