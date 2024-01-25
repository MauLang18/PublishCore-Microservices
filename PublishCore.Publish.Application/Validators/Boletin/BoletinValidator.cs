using FluentValidation;
using PublishCore.Publish.Application.Dtos.Boletin.Request;

namespace PublishCore.Publish.Application.Validators.Boletin
{
    public class BoletinValidator : AbstractValidator<BoletinRequestDto>
    {
        public BoletinValidator()
        {
            RuleFor(x => x.Nombre)
                .NotNull().WithMessage("El campo NOMBRE no puede ser nulo")
                .NotEmpty().WithMessage("El campo NOMBRE no puede estar vacio");
            RuleFor(x => x.Imagen)
                .NotNull().WithMessage("El campo IMAGEN no puede ser nulo")
                .NotEmpty().WithMessage("El campo IMAGEN no puede estar vacio");
        }
    }
}