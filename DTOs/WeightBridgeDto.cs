namespace TMS_Api.DTOs
{
    public class WeightBridgeDto
    {
        public string WeightBridgeID { get; set; } = null!;
        public string? Name { get; set; }
        public string? YardID { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }

    }
}
