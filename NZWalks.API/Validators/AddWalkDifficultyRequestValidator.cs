using FluentValidation;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Validators
{
    public class AddWalkDifficultyRequestValidator:AbstractValidator<UpdateWalkDifficultyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            RuleFor(X => X.Code).NotEmpty();
        }
    }
}
