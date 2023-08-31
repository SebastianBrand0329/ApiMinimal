using PropidadesApiMinimal.Models;

namespace PropidadesApiMinimal.Data
{
    public static class DatosPropiedad
    {
        public static List<Propiedad> listaPropiedades = new()
        {
            new Propiedad { IdPropiedad = 1,
                            Nombre = "Casa las Palmas",
                            Descripcion = "Test 1",
                            Ubicacion = "Cartagena",
                            Activa = true,
                            FechaCreacion = DateTime.Now.AddDays(-10)},

            new Propiedad { IdPropiedad = 2,
                            Nombre = "Casa laureles",
                            Descripcion = "Test 2",
                            Ubicacion = "Medellin",
                            Activa = true,
                            FechaCreacion = DateTime.Now.AddDays(-10)},

            new Propiedad { IdPropiedad = 3,
                            Nombre = "Casa los Colores",
                            Descripcion = "Test 2",
                            Ubicacion = "Bogotá",
                            Activa = true,
                            FechaCreacion = DateTime.Now.AddDays(-10)}
        };

    }
}
