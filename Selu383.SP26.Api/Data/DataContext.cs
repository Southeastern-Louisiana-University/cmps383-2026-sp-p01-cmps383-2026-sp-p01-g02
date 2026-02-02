using Microsoft.EntityFrameworkCore;
using Selu383.SP26.Api.Models;

namespace Selu383.SP26.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Location> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Location entity
            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(120);
                entity.Property(e => e.Address).IsRequired();
                entity.Property(e => e.TableCount).IsRequired();
            });

            // Seed initial data (at least 3 locations required)
            modelBuilder.Entity<Location>().HasData(
                new Location 
                { 
                    Id = 1, 
                    Name = "Hammond Coffee House", 
                    Address = "123 Oak Street, Hammond, LA 70401",
                    TableCount = 10
                },
                new Location 
                { 
                    Id = 2, 
                    Name = "University Cafe", 
                    Address = "500 W University Ave, Hammond, LA 70402",
                    TableCount = 15
                },
                new Location 
                { 
                    Id = 3, 
                    Name = "Downtown Brew", 
                    Address = "789 Main Street, Hammond, LA 70403",
                    TableCount = 8
                }
            );
        }
    }
}