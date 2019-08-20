namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientTransaction
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public int ReferenceNumber { get; set; }

        public string CardCode { get; set; }

        public string Description { get; set; }

        public double Amount { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }
    }
}
