namespace TMS_Api.DTOs
{
    public class DocumentSettingDto
    {
        public string DocCode { get; set; } = null!;
        public string? DocName { get; set; }
        public string? PCCode { get; set; }
        public Boolean? AttachRequired { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }

    }
}
