namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CartProductItem
    {
        public int Id { get; set; }

        public int TenantId { get; set; }

        public int CartId { get; set; }

        public int RemoteId { get; set; }

        public int Quantity { get; set; }

        public double Discount { get; set; }

        public double DiscountPercent { get; set; }

        public double ISV { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public int? ProductVariantId { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual ProductVariant ProductVariant { get; set; }
    }
}
