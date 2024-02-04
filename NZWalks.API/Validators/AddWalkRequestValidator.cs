using FluentValidation;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Validators
{
    public class AddWalkRequestValidator:AbstractValidator<AddWalkRequest>
    {
        public AddWalkRequestValidator()
        {
            RuleFor(X => X.Name).NotEmpty();
            RuleFor(X => X.Length).GreaterThan(0);
           
        }
    }
}
