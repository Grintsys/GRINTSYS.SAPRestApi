namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SAPRestApiContext : DbContext
    {
        public SAPRestApiContext()
            : base("name=SAPRestApiContext")
        {
        }

        public virtual DbSet<AbpTenant> AbpTenants { get; set; }
        public virtual DbSet<AbpUser> AbpUsers { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<CartProductItem> CartProductItems { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ClientDiscount> ClientDiscounts { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<ClientTransaction> ClientTransactions { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PaymentInvoiceItem> PaymentInvoiceItems { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductVariant> ProductVariants { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public virtual DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public virtual DbSet<PurchaseOrderExpense> PurchaseOrderExpenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbpUser>()
                .HasMany(e => e.AbpTenants)
                .WithOptional(e => e.AbpUser)
                .HasForeignKey(e => e.CreatorUserId);

            modelBuilder.Entity<AbpUser>()
                .HasMany(e => e.AbpTenants1)
                .WithOptional(e => e.AbpUser1)
                .HasForeignKey(e => e.DeleterUserId);

            modelBuilder.Entity<AbpUser>()
                .HasMany(e => e.AbpTenants2)
                .WithOptional(e => e.AbpUser2)
                .HasForeignKey(e => e.LastModifierUserId);

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
                .HasMany(e => e.Carts)
                .WithRequired(e => e.AbpUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AbpUser>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.AbpUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AbpUser>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.AbpUser)
                .HasForeignKey(e => e.UserId);
        }
    }
}
