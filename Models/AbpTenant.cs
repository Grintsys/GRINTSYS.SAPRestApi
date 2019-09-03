namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AbpTenant
    {
        public int Id { get; set; }

        [StringLength(1024)]
        public string ConnectionString { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public long? CreatorUserId { get; set; }

        public long? DeleterUserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletionTime { get; set; }

        public int? EditionId { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModificationTime { get; set; }

        public long? LastModifierUserId { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        public string TenancyName { get; set; }

        public string Currency { get; set; }

        public double ISV { get; set; }

        public string Language { get; set; }

        public string Logo { get; set; }

        public string GoogleUA { get; set; }

        public string SAPDatabase { get; set; }

        public string CostingCode { get; set; }
        public string CostingCode2 { get; set; }
        public virtual AbpUser AbpUser { get; set; }

        public virtual AbpUser AbpUser1 { get; set; }

        public virtual AbpUser AbpUser2 { get; set; }
    }
}
