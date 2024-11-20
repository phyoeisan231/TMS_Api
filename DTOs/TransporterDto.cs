namespace TMS_Api.DTOs
{
    public class TransporterDto
    {
        public string TransporterID { get; set; } = null!;
        public int? SrNo { get; set; }
        public string? TransporterName { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public string? ContactPerson { get; set; }
        public string? TypeID { get; set; }
        public Boolean? Active { get; set; }
        public Boolean? IsBlack { get; set; }
        public DateTime? BlackDate { get; set; }
        public DateTime? BlackRemovedDate { get; set; }
        public string? Remarks { get; set; }
        public string? BlackReason { get; set; }
        public string? BlackRemovedReason { get; set; }
        public string? NotUseReason { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? SAPID { get; set; }

    }
}
