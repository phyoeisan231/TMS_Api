namespace TMS_Api.DTOs
{
    public class OperationDto
    {
        public int? InRegNo { get; set; }
        public int? GRNNo { get; set; }
        public int? GDNNo { get; set; }
        public DateTime? OptStartDate { get; set; }//OperationStart
        public DateTime? OptEndDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
