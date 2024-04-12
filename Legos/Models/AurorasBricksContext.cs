using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Legos.Models;

public partial class AurorasBricksContext : DbContext
{
    public AurorasBricksContext()
    {
    }

    public AurorasBricksContext(DbContextOptions<AurorasBricksContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserRole> AspNetUserRoles { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<LineItem> LineItems { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:myfreesqldbserveris113.database.windows.net,1433;Initial Catalog=auroras_bricks;Persist Security Info=False;User ID=aurorasbricks;Password=Iamthemasterbuilder!999;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ConcurrencyStamp)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ClaimType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClaimValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoleId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ConcurrencyStamp)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LockoutEnd)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedEmail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NormalizedUserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(128)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SecurityStamp)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ClaimType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ClaimValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.LoginProvider)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProviderDisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProviderKey)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetUserRole>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.RoleId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.LoginProvider)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Value)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("customer");

            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Birthdate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("birthdate");
            entity.Property(e => e.Countryofresidence)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("countryofresidence");
            entity.Property(e => e.CustomerId).HasColumnName("customer_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Lastname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("lastname");
        });

        modelBuilder.Entity<LineItem>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("line_items");

            entity.Property(e => e.ProductId).HasColumnName("productID");
            entity.Property(e => e.Qty).HasColumnName("qty");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_ID");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("orders");

            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.Bank)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("bank");
            entity.Property(e => e.Countryoftransaction)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("countryoftransaction");
            entity.Property(e => e.CustomerId).HasColumnName("customerID");
            entity.Property(e => e.Date)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("date");
            entity.Property(e => e.Dayofweek)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dayofweek");
            entity.Property(e => e.Entrymode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("entrymode");
            entity.Property(e => e.Fraud).HasColumnName("fraud");
            entity.Property(e => e.Shippingaddress)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("shippingaddress");
            entity.Property(e => e.Time).HasColumnName("time");
            entity.Property(e => e.TransactionId).HasColumnName("transaction_ID");
            entity.Property(e => e.Typeofcard)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("typeofcard");
            entity.Property(e => e.Typeoftransaction)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("typeoftransaction");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("products");

            entity.Property(e => e.Category)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.Description)
                .HasMaxLength(4096)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Imglink)
                .HasMaxLength(256)
                .IsUnicode(false)
                .HasColumnName("imglink");
            entity.Property(e => e.Name)
                .HasMaxLength(64)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Numparts).HasColumnName("numparts");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Primarycolor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("primarycolor");
            entity.Property(e => e.ProductId).HasColumnName("product_ID");
            entity.Property(e => e.Rec1).HasColumnName("rec1");
            entity.Property(e => e.Rec2).HasColumnName("rec2");
            entity.Property(e => e.Rec3).HasColumnName("rec3");
            entity.Property(e => e.Rec4).HasColumnName("rec4");
            entity.Property(e => e.Rec5).HasColumnName("rec5");
            entity.Property(e => e.Secondarycolor)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("secondarycolor");
            entity.Property(e => e.Year).HasColumnName("year");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
