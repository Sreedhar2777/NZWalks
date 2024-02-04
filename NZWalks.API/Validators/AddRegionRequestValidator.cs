using FluentValidation;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Validators
{
    public class AddRegionRequestValidator : AbstractValidator<AddRegionRequest>
    {
        public AddRegionRequestValidator()
        {
            RuleFor(X => X.Code).NotEmpty();
            RuleFor(X => X.Name).NotEmpty();
            RuleFor(X => X.Area).GreaterThan(0);
            RuleFor(X => X.Population).GreaterThanOrEqualTo(0);
        }
    }
}
