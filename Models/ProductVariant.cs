namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductVariant
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public int ItemGroup { get; set; }

        public int ProductId { get; set; }

        public int ColorId { get; set; }

        public int SizeId { get; set; }

        [Required]
        public string Code { get; set; }

        public int Quantity { get; set; }

        public int IsCommitted { get; set; }

        public double Price { get; set; }

        public string Currency { get; set; }

        public string WareHouseCode { get; set; }

        public string ImageUrl { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public virtual Product Product { get; set; }
    }
}
