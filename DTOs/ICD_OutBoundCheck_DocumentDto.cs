namespace TMS_Api.DTOs
{
    public class ICD_OutBoundCheck_DocumentDto
    {
        public int OutRegNo { get; set; } = 0!;
        public string? DocCode { get; set; }
        public string? DocName { get; set; }
        public Boolean? CheckStatus { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedUser { get; set; }
        public string? CreatedUser { get; set; }
    }
}
