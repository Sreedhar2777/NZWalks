using FluentValidation;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Validators
{
    public class UpdateWalkRequestValidator:AbstractValidator<UpdateWalkRequest>
    {
        public UpdateWalkRequestValidator()
        {

            RuleFor(X => X.Name).NotEmpty();
            RuleFor(X => X.Length).GreaterThan(0);
        }
    }
}
