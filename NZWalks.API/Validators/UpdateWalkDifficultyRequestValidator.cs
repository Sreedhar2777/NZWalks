using FluentValidation;
using NZWalks.API.Model.DTO;

namespace NZWalks.API.Validators
{
    public class UpdateWalkDifficultyRequestValidator:AbstractValidator<UpdateWalkDifficultyRequest>
    {
        public UpdateWalkDifficultyRequestValidator()
        {
            RuleFor(X => X.Code).NotEmpty();
        }
    }
}
