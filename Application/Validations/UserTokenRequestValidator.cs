using System.Threading.Tasks;
using Domain.Dto;
using FluentValidation;

namespace Application.Validations
{
    public class UserTokenRequestValidator : AbstractValidator<UserTokenRequest>
    {


        public UserTokenRequestValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("El userName es obligatorio")
                .MinimumLength(2).WithMessage("El nombre debe tener al menos 2 caracteres");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("El password es obligatorio")
                .MinimumLength(2).WithMessage("El password debe tener al menos 2 caracteres");

       


        }

    }
}
