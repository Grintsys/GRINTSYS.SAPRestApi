namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Payment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Payment()
        {
            PaymentInvoiceItems = new HashSet<PaymentInvoiceItem>();
        }

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

        public int Type { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime PayedDate { get; set; }

        public string CardCode { get; set; }

        public virtual AbpUser AbpUser { get; set; }

        public virtual Bank Bank { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PaymentInvoiceItem> PaymentInvoiceItems { get; set; }
    }
}
