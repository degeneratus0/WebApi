using Microsoft.EntityFrameworkCore;
using WebApi.Models.TestData;

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
            modelBuilder.Entity<Measure>().HasData(
                WeighingsTestData.TestMeasures
                );
            modelBuilder.Entity<Weighing>().HasData(
                WeighingsTestData.TestWeighings
                );
        }
    }
}
