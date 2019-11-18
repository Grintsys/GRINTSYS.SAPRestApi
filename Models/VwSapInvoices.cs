namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public partial class VwSapInvoices
    {
        public VwSapInvoices()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }

        public int TenantId { get; set; }

        public string RemoteId { get; set; }

        public int Status { get; set; }

        public long UserId { get; set; }

        public string Comment { get; set; }

        public string LastMessage { get; set; }

        public int Series { get; set; }

        public string DeliveryDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public string CardCode { get; set; }

        public virtual AbpUser AbpUser { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}