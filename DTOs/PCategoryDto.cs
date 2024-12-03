namespace TMS_Api.DTOs
{
    public class PCategoryDto
    {
        public string PCCode { get; set; } = null!;
        public string? CategoryName { get; set; }
        public Boolean? InboundWeight { get; set; }
        public Boolean? OutboundWeight { get; set; }
        public string? GroupName { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }

    }
}
