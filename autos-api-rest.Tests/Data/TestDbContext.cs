using Microsoft.EntityFrameworkCore;
using autos_api_rest.Models;

namespace autos_api_rest.Tests.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options)
            : base(options)
        {
        }

        public DbSet<MarcaAuto> MarcasAutos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MarcaAuto>().ToTable("MarcasAutos");

            modelBuilder.Entity<MarcaAuto>().HasData(
                new MarcaAuto { Id = 1, Marca = "Hyundai" },
                new MarcaAuto { Id = 2, Marca = "Nissan" },
                new MarcaAuto { Id = 3, Marca = "Toyota" },
                new MarcaAuto { Id = 4, Marca = "Ford" },
                new MarcaAuto { Id = 5, Marca = "Chevrolet" }
            );
        }
    }
}
