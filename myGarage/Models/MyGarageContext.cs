using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace myGarage.Models;

public partial class MyGarageContext : DbContext
{
    public MyGarageContext()
    {
    }

    public MyGarageContext(DbContextOptions<MyGarageContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserXcar> UserXcars { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:mygarage.database.windows.net,1433;Initial Catalog=myGarage;Persist Security Info=False;User ID=octavian;Password=S3@tleon5F;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Model>(entity =>
        {
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserXcar>(entity =>
        {
            entity.HasKey(e => e.RecordId);

            entity.ToTable("UserXCars");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.BrandId).HasColumnName("BrandID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
