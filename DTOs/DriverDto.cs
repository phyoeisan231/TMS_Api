namespace TMS_Api.DTOs
{
    public class DriverDto
    {
        public string LicenseNo { get; set; } = null!;
        public string? NRC { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? LicenseExpiration { get; set; }
        public string? LicenseClass { get; set; }
        public string? Transporter { get; set; }
        public string? Remarks { get; set; }
        public Boolean? IsBlack { get; set; }
        public DateTime? BlackDate { get; set; }
        public DateTime? BlackRemovedDate { get; set; }
        public string? BlackReason { get; set; }
        public string? BlackRemovedReason { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }

    }
}
