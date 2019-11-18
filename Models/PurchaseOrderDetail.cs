namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class PurchaseOrderDetail
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public String ItemCode { get; set; }
        public String Dscription { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
        public String LineCurrency { get; set; }
        public String TaxCode { get; set; }
        public Decimal LineTotal { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}