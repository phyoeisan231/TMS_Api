namespace TMS_Api.DTOs
{
    public class WeightBridgeDto
    {
        public string WeightBridgeID { get; set; } = null!;
        public string? Name { get; set; }
        public string? YardID { get; set; }
        public Boolean? Active { get; set; }
        public string? UpdatedUser { get; set; }
        public string? CreatedUser { get; set; }

    }
}
