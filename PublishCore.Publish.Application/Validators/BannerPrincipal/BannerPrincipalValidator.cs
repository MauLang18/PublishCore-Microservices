using FluentValidation;
using PublishCore.Publish.Application.Dtos.BannerPrincipal.Request;

namespace PublishCore.Publish.Application.Validators.BannerPrincipal
{
    public class BannerPrincipalValidator : AbstractValidator<BannerPrincipalRequestDto>
    {
        public BannerPrincipalValidator()
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