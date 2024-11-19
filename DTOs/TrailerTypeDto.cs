namespace TMS_Api.DTOs
{
    public class TrailerTypeDto
    {
        public int TypeCode { get; set; } = 0!;
        public string? Description { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
