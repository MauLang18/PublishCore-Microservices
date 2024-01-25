using FluentValidation;
using PublishCore.Publish.Application.Dtos.ServicioBeneficio.Request;

namespace PublishCore.Publish.Application.Validators.ServicioBeneficio
{
    public class ServicioBeneficioValidator : AbstractValidator<ServicioBeneficioRequestDto>
    {
        public ServicioBeneficioValidator()
        {
            RuleFor(x => x.Titulo)
                .NotNull().WithMessage("El campo TITULO no puede ser nulo")
                .NotEmpty().WithMessage("El campo TITULO no puede estar vacio");
            RuleFor(x => x.Descripcion)
                .NotNull().WithMessage("El campo DESCRIPCION no puede ser nulo")
                .NotEmpty().WithMessage("El campo DESCRIPCION no puede estar vacio");
            RuleFor(x => x.Imagen)
                .NotNull().WithMessage("El campo IMAGEN no puede ser nulo")
                .NotEmpty().WithMessage("El campo IMAGEN no puede estar vacio");
        }
    }
}