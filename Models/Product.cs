namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            ProductVariants = new HashSet<ProductVariant>();
        }

        public int Id { get; set; }

        public int TenantId { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public string Season { get; set; }

        public string Description { get; set; }

        public string MainImage { get; set; }

        public string MainImageHighRes { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductVariant> ProductVariants { get; set; }
    }
}
