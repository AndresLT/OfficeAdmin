using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OfficeAdmin_API.Models
{
    public partial class TestDBContext : DbContext
    {
        private readonly IConfiguration _config;
        public TestDBContext(IConfiguration config)
        {
            _config = config;
        }

        public TestDBContext(DbContextOptions<TestDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Currency> Currencies { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Office> Offices { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config["ConnectionString"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency", "test_al");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log", "test_al");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.LogDate)
                    .HasColumnType("date")
                    .HasColumnName("log_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Log__user_id__62458BBE");
            });

            modelBuilder.Entity<Office>(entity =>
            {
                entity.ToTable("Office", "test_al");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Code).HasColumnName("code");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("date")
                    .HasColumnName("create_date");

                entity.Property(e => e.CreateUser).HasColumnName("create_user");

                entity.Property(e => e.Currency).HasColumnName("currency");

                entity.Property(e => e.Description)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Identification)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("identification");

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("date")
                    .HasColumnName("modify_date");

                entity.Property(e => e.ModifyUser).HasColumnName("modify_user");

                entity.HasOne(d => d.CreateUserNavigation)
                    .WithMany(p => p.OfficeCreateUserNavigations)
                    .HasForeignKey(d => d.CreateUser)
                    .HasConstraintName("FK__Office__create_u__5E74FADA");

                entity.HasOne(d => d.CurrencyNavigation)
                    .WithMany(p => p.Offices)
                    .HasForeignKey(d => d.Currency)
                    .HasConstraintName("FK__Office__currency__5D80D6A1");

                entity.HasOne(d => d.ModifyUserNavigation)
                    .WithMany(p => p.OfficeModifyUserNavigations)
                    .HasForeignKey(d => d.ModifyUser)
                    .HasConstraintName("FK__Office__modify_u__5F691F13");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "test_al");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
