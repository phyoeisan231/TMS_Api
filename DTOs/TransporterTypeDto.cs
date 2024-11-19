namespace TMS_Api.DTOs
{
    public class TransporterTypeDto
    {
        public int TypeCode { get; set; } = 0!;
        public string? TypeName { get; set; }
        public string? Remarks { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }

    }
}
