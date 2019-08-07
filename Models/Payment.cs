namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string DocEntry { get; set; }

        public double PayedAmount { get; set; }

        public string LastMessage { get; set; }

        public int Status { get; set; }

        [StringLength(150)]
        public string Comment { get; set; }

        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public long UserId { get; set; }

        public int BankId { get; set; }

        public int InvoiceId { get; set; }

        public int Type { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PayedDate { get; set; }

        public virtual AbpUser AbpUser { get; set; }

        public virtual Bank Bank { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
