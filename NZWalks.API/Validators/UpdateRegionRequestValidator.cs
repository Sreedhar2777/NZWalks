using FluentValidation;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Validators
{
    public class UpdateRegionRequestValidator:AbstractValidator<UpdateRegionRequest>
    {
        public UpdateRegionRequestValidator()
        {
            RuleFor(X => X.Code).NotEmpty();
            RuleFor(X => X.Name).NotEmpty();
            RuleFor(X => X.Area).GreaterThan(0);
            RuleFor(X => X.Population).GreaterThanOrEqualTo(0);
        }


    }
}
