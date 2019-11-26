namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class PurchaseOrder
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public int RemoteId { get; set; }
        public int Status { get; set; }
        public String LastMessage { get; set; }
        public long UserId { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }
        public DateTime DocCreateDate { get; set; }
        public DateTime DocDate { get; set; }
        public String CardCode { get; set; }
        public String CardName { get; set; }
        public Decimal DocTotal { get; set; }
        public Decimal DocTotalExp { get; set; }
        public String DocCurrency { get; set; }
        public String Comments { get; set; }
        public int SlpCode { get; set; }
        public Guid U_M2_UUID { get; set; }
        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual ICollection<PurchaseOrderExpense> PurchaseOrderExpenses { get; set; }
    }
}