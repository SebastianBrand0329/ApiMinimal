using AutoMapper;
using PropidadesApiMinimal.Models;
using PropidadesApiMinimal.Models.DTo;

namespace PropidadesApiMinimal.Mapper
{
    public class ConfiguracionMapper : Profile
    {
        public ConfiguracionMapper()
        {
            CreateMap<Propiedad, CrearPropiedadDto>().ReverseMap();
            CreateMap<Propiedad, PropiedadDto>().ReverseMap();
        }
    }
}
