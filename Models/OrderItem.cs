namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OrderItem
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public int OrderId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public double Discount { get; set; }

        public double DiscountPercent { get; set; }

        public double TaxValue { get; set; }

        public string TaxCode { get; set; }

        public string WarehouseCode { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public string Code { get; set; }

        public virtual Order Order { get; set; }
    }
}
