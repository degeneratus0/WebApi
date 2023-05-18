using Microsoft.EntityFrameworkCore;

namespace WebApi.Models
{
    internal class WeighingsContext : DbContext
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
                .HasOne(s => s.Measure)
                .WithMany(g => g.Weighings)
                .HasForeignKey(s => s.IdMeasure)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Measure>()
                .HasKey(e => e.Id);
            modelBuilder.Entity<Weighing>()
                .HasKey(e => e.Id);
        }
    }
}
