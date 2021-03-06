namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductVariant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductVariant()
        {
            CartProductItems = new HashSet<CartProductItem>();
        }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CartProductItem> CartProductItems { get; set; }

        public virtual Product Product { get; set; }
    }
}
