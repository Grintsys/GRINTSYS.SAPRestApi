namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("paymentInvoiceItems")]
    public partial class PaymentInvoiceItem
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public int PaymentId { get; set; }

        public string DocumentCode { get; set; }

        public double TotalAmount { get; set; }

        public double BalanceDue { get; set; }

        public double PayedAmount { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public int DocEntry { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
