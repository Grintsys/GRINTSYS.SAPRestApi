namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Invoice
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public int DocEntry { get; set; }

        public string DueDate { get; set; }

        public double TotalAmount { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public double BalanceDue { get; set; }

        public int ClientId { get; set; }

        public string DocumentCode { get; set; }

        public double OverdueDays { get; set; }

        public virtual Client Client { get; set; }
    }
}
