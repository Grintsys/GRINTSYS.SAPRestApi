namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class PurchaseOrderExpense
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int ExpnsCode { get; set; }
        public String Comments { get; set; }
        public String TaxCode { get; set; }
        public Decimal LineVat { get; set; }
        public String DistrbMthd { get; set; }
        public Decimal LineTotal { get; set; }
        public String LineCurrency { get; set; }
        public String U_TipoA { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}