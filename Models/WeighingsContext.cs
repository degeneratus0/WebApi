using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class WeighingsContext : DbContext
    {
        public DbSet<Weighing> Weighings { get; set; }
        public DbSet<Measure> Measures { get; set; }
        public WeighingsContext(DbContextOptions<WeighingsContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weighing>()
                .HasOne<Measure>(s => s.Measure)
                .WithMany(g => g.Weighings)
                .HasForeignKey(s => s.idMeasure)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Measure>()
                .HasKey(e => e.IDMeasure);
            modelBuilder.Entity<Weighing>()
                .HasKey(e => e.IDWeighing);
        }
    }
}
