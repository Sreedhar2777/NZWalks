using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class WalkDifficultyProfile:Profile
    {
        public WalkDifficultyProfile()
        {
            CreateMap<Model.Domain.WalkDifficulty, Model.DTO.WalkDifficulty>()
            .ReverseMap();
        }
    }
}
