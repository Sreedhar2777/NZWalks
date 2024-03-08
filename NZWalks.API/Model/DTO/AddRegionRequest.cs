    namespace NZWalks.API.Model.DTO
{
    public class AddRegionRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public Double Area { get; set; }
        public Double Lat { get; set; }
        public Double Long { get; set; }
        public long Population { get; set; }
    }
}
