using FluentValidation;
using PublishCore.Auth.Application.Dtos.Empresa.Request;

namespace PublishCore.Auth.Application.Validators.Empresa
{
    public class EmpresaValidator : AbstractValidator<EmpresaRequestDto>
    {
        public EmpresaValidator()
        {
            RuleFor(x => x.Empresa)
                .NotNull().WithMessage("El campo EMPRESA no puede ser nulo")
                .NotEmpty().WithMessage("El campo EMPRESA no puede estar vacio");
        }
    }
}