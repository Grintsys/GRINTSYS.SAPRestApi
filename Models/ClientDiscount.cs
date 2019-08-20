namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ClientDiscount
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public string CardCode { get; set; }

        public int ItemGroup { get; set; }

        public double Discount { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }
    }
}
