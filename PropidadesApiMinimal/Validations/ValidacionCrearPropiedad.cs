using FluentValidation;
using PropidadesApiMinimal.Models.DTo;

namespace PropidadesApiMinimal.Validations
{
    public class ValidacionCrearPropiedad : AbstractValidator<CrearPropiedadDto>
    {
        public ValidacionCrearPropiedad()
        {
            RuleFor(model => model.Nombre).NotEmpty();
            RuleFor(model => model.Descripcion).NotEmpty();
            RuleFor(model => model.Ubicacion).NotEmpty();
        }
    }
}
