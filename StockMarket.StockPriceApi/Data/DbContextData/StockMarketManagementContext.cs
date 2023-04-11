using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StockMarket.StockPriceApi.Data.DataModel;

namespace StockMarket.StockPriceApi.Data.DbContextData;

public partial class StockMarketManagementContext : DbContext
{
    public StockMarketManagementContext()
    {
    }

    public StockMarketManagementContext(DbContextOptions<StockMarketManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CompanyDetail> CompanyDetails { get; set; }

    public virtual DbSet<StockExchange> StockExchanges { get; set; }

    public virtual DbSet<StockPrice> StockPrices { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyDetail>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK_CompanyDetails_CompanyId");

            entity.Property(e => e.CompanyCeoName).HasMaxLength(1000);
            entity.Property(e => e.CompanyCode).HasMaxLength(50);
            entity.Property(e => e.CompanyName).HasMaxLength(1000);
            entity.Property(e => e.CompanyTurnOver).HasColumnType("decimal(38, 4)");
            entity.Property(e => e.CompanyWebsiteUrl).HasMaxLength(1000);
            entity.Property(e => e.CreatedBy).HasMaxLength(1000);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedBy).HasMaxLength(1000);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.StockExchange).WithMany(p => p.CompanyDetails)
                .HasForeignKey(d => d.StockExchangeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StockExchange_Id");
        });

        modelBuilder.Entity<StockExchange>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_StockExchange_Id");

            entity.ToTable("StockExchange");

            entity.Property(e => e.StockExchangeName).HasMaxLength(1000);
        });

        modelBuilder.Entity<StockPrice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_StockPrice_Id");

            entity.ToTable("StockPrice");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.StockPriceValue).HasColumnType("decimal(38, 4)");

            entity.HasOne(d => d.Company).WithMany(p => p.StockPrices)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CompanyDetails_CompanyId");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User_Id");

            entity.ToTable("User");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
