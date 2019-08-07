namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SAPRestApiContext : DbContext
    {
        public SAPRestApiContext()
            : base("name=SAPMiddlewareConnection")
        {
        }

        public virtual DbSet<AbpUser> AbpUsers { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductVariant> ProductVariants { get; set; }
        public virtual DbSet<Size> Sizes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbpUser>()
                .HasMany(e => e.AbpUsers1)
                .WithOptional(e => e.AbpUser1)
                .HasForeignKey(e => e.CreatorUserId);

            modelBuilder.Entity<AbpUser>()
                .HasMany(e => e.AbpUsers11)
                .WithOptional(e => e.AbpUser2)
                .HasForeignKey(e => e.DeleterUserId);

            modelBuilder.Entity<AbpUser>()
                .HasMany(e => e.AbpUsers12)
                .WithOptional(e => e.AbpUser3)
                .HasForeignKey(e => e.LastModifierUserId);

            modelBuilder.Entity<AbpUser>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.AbpUser)
                .HasForeignKey(e => e.UserId);
        }
    }
}
