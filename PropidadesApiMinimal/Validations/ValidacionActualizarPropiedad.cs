using FluentValidation;
using PropidadesApiMinimal.Models.DTo;

namespace PropidadesApiMinimal.Validations
{
    public class ValidacionActualizarPropiedad : AbstractValidator<ActualizarPropiedadDto>
    {
        public ValidacionActualizarPropiedad()
        {
            RuleFor(model => model.IdPropiedad).NotEmpty().GreaterThan(0);
            RuleFor(model => model.Nombre).NotEmpty();
            RuleFor(model => model.Descripcion).NotEmpty();
            RuleFor(model => model.Ubicacion).NotEmpty();
        }
    }
}
