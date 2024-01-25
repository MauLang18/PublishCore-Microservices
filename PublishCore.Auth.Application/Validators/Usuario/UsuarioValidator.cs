using FluentValidation;
using PublishCore.Auth.Application.Dtos.Usuario.Request;

namespace PublishCore.Auth.Application.Validators.Usuario
{
    public class UsuarioValidator : AbstractValidator<UsuarioRequestDto>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Usuario)
                .NotNull().WithMessage("El campo USUARIO no puede ser nulo")
                .NotEmpty().WithMessage("El campo USUARIO no puede estar vacio");
            RuleFor(x => x.Pass)
                .NotNull().WithMessage("El campo PASS no puede ser nulo")
                .NotEmpty().WithMessage("El campo PASS no puede estar vacio");
        }
    }
}