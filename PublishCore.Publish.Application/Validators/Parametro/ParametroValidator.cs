using FluentValidation;
using PublishCore.Publish.Application.Dtos.Parametro.Request;

namespace PublishCore.Publish.Application.Validators.Parametro
{
    public class ParametroValidator : AbstractValidator<ParametroRequestDto>
    {
        public ParametroValidator()
        {
            RuleFor(x => x.Parametro)
                .NotNull().WithMessage("El campo PARAMETRO no puede ser nulo")
                .NotEmpty().WithMessage("El campo PARAMETRO no puede estar vacio");
            RuleFor(x => x.Descripcion)
                .NotNull().WithMessage("El campo DESCRIPCION no puede ser nulo")
                .NotEmpty().WithMessage("El campo DESCRIPCION no puede estar vacio");
            RuleFor(x => x.Valor)
                .NotNull().WithMessage("El campo VALOR no puede ser nulo")
                .NotEmpty().WithMessage("El campo VALOR no puede estar vacio");
        }
    }
}