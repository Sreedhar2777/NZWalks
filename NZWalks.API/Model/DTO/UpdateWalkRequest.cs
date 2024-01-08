namespace NZWalks.API.Model.DTO
{
    public class UpdateWalkRequest
    {

        public string Name { get; set; }
        public Double Length { get; set; }
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }
    }
}
