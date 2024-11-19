namespace TMS_Api.DTOs
{
    public class GateDto
    {
        public int GateId { get; set; } = 0!;
        public string? Name { get; set; }
        public string? Location { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }

    }
}
