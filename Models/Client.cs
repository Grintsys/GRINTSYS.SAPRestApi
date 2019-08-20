namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }

        public int TenantId { get; set; }

        public string Name { get; set; }

        public string Dimension { get; set; }

        public string CardCode { get; set; }

        public string PhoneNumber { get; set; }

        public double CreditLimit { get; set; }

        public double Balance { get; set; }

        public double InOrders { get; set; }

        public string PayCondition { get; set; }

        public string Address { get; set; }

        public string RTN { get; set; }

        public double PastDue { get; set; }

        public string ContactPerson { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
