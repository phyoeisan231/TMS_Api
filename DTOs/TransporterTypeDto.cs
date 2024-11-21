namespace TMS_Api.DTOs
{
    public class TransporterTypeDto
    {
        public string TypeID { get; set; } = null!;
        public string? Description { get; set; }
        public string? Remarks { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }

    }
}
