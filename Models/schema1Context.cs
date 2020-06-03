using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApi
{
    public partial class schema1Context : DbContext
    {
        public schema1Context()
        {
        }

        public schema1Context(DbContextOptions<schema1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Maintable> Maintable { get; set; }
        public virtual DbSet<Reftable> Reftable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=schema1;user=root;password=1488", x => x.ServerVersion("8.0.15-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Maintable>(entity =>
            {
                entity.HasKey(e => e.IdmainTable)
                    .HasName("PRIMARY");

                entity.ToTable("maintable");

                entity.HasIndex(e => e.IdReference)
                    .HasName("Reference_idx");

                entity.HasIndex(e => e.IdmainTable)
                    .HasName("idTable1_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdmainTable)
                    .HasColumnName("IDMainTable")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Data)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");

                entity.Property(e => e.IdReference)
                    .HasColumnName("idReference")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdReferenceNavigation)
                    .WithMany(p => p.Maintable)
                    .HasForeignKey(d => d.IdReference)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("IDRefTable");
            });

            modelBuilder.Entity<Reftable>(entity =>
            {
                entity.HasKey(e => e.IdrefTable)
                    .HasName("PRIMARY");

                entity.ToTable("reftable");

                entity.HasIndex(e => e.IdrefTable)
                    .HasName("idnew_table_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.IdrefTable)
                    .HasColumnName("IDRefTable")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Reference)
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_0900_ai_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
