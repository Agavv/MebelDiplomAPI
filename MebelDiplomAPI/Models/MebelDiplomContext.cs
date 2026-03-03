using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MebelDiplomAPI.Models;

public partial class MebelDiplomContext : DbContext
{
    public MebelDiplomContext()
    {
    }

    public MebelDiplomContext(DbContextOptions<MebelDiplomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SystemLog> SystemLogs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Cart__51BCD797F44763E9");

            entity.HasOne(d => d.User).WithOne(p => p.Cart).HasConstraintName("FK__Cart__UserID__787EE5A0");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.CartItemId).HasName("PK__CartItem__488B0B2A93063431");

            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems).HasConstraintName("FK__CartItems__CartI__7E37BEF6");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CartItems__Produ__7F2BE32F");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A2BD94C100A");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory).HasConstraintName("FK__Categorie__Paren__4BAC3F29");
        });

        modelBuilder.Entity<DeliveryMethod>(entity =>
        {
            entity.HasKey(e => e.DeliveryMethodId).HasName("PK__Delivery__7B03A3A2F91C5BC4");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PK__Favorite__CE74FAF5D7620CE5");

            entity.HasOne(d => d.Product).WithMany(p => p.Favorites).HasConstraintName("FK__Favorites__Produ__03F0984C");

            entity.HasOne(d => d.User).WithMany(p => p.Favorites).HasConstraintName("FK__Favorites__UserI__02FC7413");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BAF6C488020");

            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Address).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__AddressI__693CA210");

            entity.HasOne(d => d.DeliveryMethod).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Delivery__6754599E");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__PaymentM__68487DD7");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__StatusID__66603565");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__UserID__656C112C");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED06A14D64CB04");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("FK__OrderItem__Order__6EF57B66");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Produ__6FE99F9F");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__OrderSta__C8EE204320B036CE");
        });

        modelBuilder.Entity<OrderStatusHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId).HasName("PK__OrderSta__4D7B4ADDB81BF93D");

            entity.Property(e => e.ChangedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderStatusHistories).HasConstraintName("FK__OrderStat__Order__73BA3083");

            entity.HasOne(d => d.Status).WithMany(p => p.OrderStatusHistories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderStat__Statu__74AE54BC");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PK__PaymentM__DC31C1F3A4FBFFBC");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PK__Permissi__EFA6FB0F42ABC089");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6ED013B07F8");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Products__Catego__5CD6CB2B");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ProductI__7516F4ECF6E74A0F");

            entity.HasIndex(e => e.ProductId, "UX_Product_PrimaryImage")
                .IsUnique()
                .HasFilter("([IsPrimary]=(1))");

            entity.HasOne(d => d.Product).WithOne(p => p.ProductImage).HasConstraintName("FK__ProductIm__Produ__60A75C0F");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79AEB9A4EB21");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__Product__0A9D95DB");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reviews__UserID__0B91BA14");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE3A773C98C4");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .HasConstraintName("FK__RolePermi__Permi__3E52440B"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK__RolePermi__RoleI__3D5E1FD2"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId").HasName("PK__RolePerm__6400A18A4C2C13EF");
                        j.ToTable("RolePermissions");
                        j.IndexerProperty<int>("RoleId").HasColumnName("RoleID");
                        j.IndexerProperty<int>("PermissionId").HasColumnName("PermissionID");
                    });
        });

        modelBuilder.Entity<SystemLog>(entity =>
        {
            entity.HasKey(e => e.LogId).HasName("PK__SystemLo__5E5499A87EBB7469");

            entity.Property(e => e.LogDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.User).WithMany(p => p.SystemLogs).HasConstraintName("FK__SystemLog__UserI__0F624AF8");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC71E27517");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleID__5165187F");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__UserAddr__091C2A1B9F8C6027");

            entity.Property(e => e.IsPrimary).HasDefaultValue(false);

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses).HasConstraintName("FK__UserAddre__UserI__5535A963");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
