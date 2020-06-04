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
        public WeighingsContext(DbContextOptions<WeighingsContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weighing>(entity =>
            {
                entity.HasKey(e => e.IDWeighing);
            });
        }
    }
}
