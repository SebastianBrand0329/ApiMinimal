using Microsoft.EntityFrameworkCore;
using PropidadesApiMinimal.Models;

namespace PropidadesApiMinimal.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Propiedad> propiedads { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Propiedad>().HasData
                (
                    new Propiedad
                    {
                        IdPropiedad = 1,
                        Nombre = "Casa las Palmas",
                        Descripcion = "Test 1",
                        Ubicacion = "Cartagena",
                        Activa = true,
                        FechaCreacion = DateTime.Now
                    },

                new Propiedad
                {
                    IdPropiedad = 2,
                    Nombre = "Casa laureles",
                    Descripcion = "Test 2",
                    Ubicacion = "Medellin",
                    Activa = true,
                    FechaCreacion = DateTime.Now
                },

                new Propiedad
                {
                    IdPropiedad = 3,
                    Nombre = "Casa los Colores",
                    Descripcion = "Test 2",
                    Ubicacion = "Bogotá",
                    Activa = true,
                    FechaCreacion = DateTime.Now
                }


              );
        }
    }
}
