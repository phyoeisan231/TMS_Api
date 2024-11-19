namespace TMS_Api.DTOs
{
    public class TrailerDto
    {
        public string VehicleRegNo { get; set; } = null!;
        public decimal? UnladenWeight { get; set; }
        public string? Remarks { get; set; }
        public float? Flowrate { get; set; }
        public string? TrailerType { get; set; }
        public string? Transporter { get; set; }
        public Boolean? IsBlack { get; set; }
        public Boolean? Active { get; set; }
        public string? LoadingType { get; set; }
        public DateTime? BlackDate { get; set; }
        public DateTime? BlackRemovedDate { get; set; }
        public int? CompartmentNo { get; set; }
        public string? DriverLicenseNo { get; set; }
        public DateTime? LastLoadingDate { get; set; }
        public string? VehicleBackRegNo { get; set; }
        public string? BlackReason { get; set; }
        public string? BlackRemovedReason { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
    }
}
