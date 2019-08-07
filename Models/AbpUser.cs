namespace GRINTSYS.SAPRestApi.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AbpUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AbpUser()
        {
            AbpTenants = new HashSet<AbpTenant>();
            AbpTenants1 = new HashSet<AbpTenant>();
            AbpTenants2 = new HashSet<AbpTenant>();
            AbpUsers1 = new HashSet<AbpUser>();
            AbpUsers11 = new HashSet<AbpUser>();
            AbpUsers12 = new HashSet<AbpUser>();
            Orders = new HashSet<Order>();
            Payments = new HashSet<Payment>();
        }

        public long Id { get; set; }

        public int AccessFailedCount { get; set; }

        [StringLength(64)]
        public string AuthenticationSource { get; set; }

        [StringLength(128)]
        public string ConcurrencyStamp { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreationTime { get; set; }

        public long? CreatorUserId { get; set; }

        public long? DeleterUserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletionTime { get; set; }

        [Required]
        [StringLength(256)]
        public string EmailAddress { get; set; }

        [StringLength(328)]
        public string EmailConfirmationCode { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public bool IsLockoutEnabled { get; set; }

        public bool IsPhoneNumberConfirmed { get; set; }

        public bool IsTwoFactorEnabled { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LastModificationTime { get; set; }

        public long? LastModifierUserId { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? LockoutEndDateUtc { get; set; }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [Required]
        [StringLength(256)]
        public string NormalizedEmailAddress { get; set; }

        [Required]
        [StringLength(256)]
        public string NormalizedUserName { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        [StringLength(328)]
        public string PasswordResetCode { get; set; }

        [StringLength(32)]
        public string PhoneNumber { get; set; }

        [StringLength(128)]
        public string SecurityStamp { get; set; }

        [Required]
        [StringLength(64)]
        public string Surname { get; set; }

        public int? TenantId { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public int CollectId { get; set; }

        public string PrintBluetoothAddress { get; set; }

        public int SalesPersonId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AbpTenant> AbpTenants { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AbpTenant> AbpTenants1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AbpTenant> AbpTenants2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AbpUser> AbpUsers1 { get; set; }

        public virtual AbpUser AbpUser1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AbpUser> AbpUsers11 { get; set; }

        public virtual AbpUser AbpUser2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AbpUser> AbpUsers12 { get; set; }

        public virtual AbpUser AbpUser3 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
