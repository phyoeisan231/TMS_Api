
namespace TMS_Api.DTOs
{
    public class TruckDto
    {
        public string VehicleRegNo { get; set; } = null!;
        public string? ContainerType { get; set; }
        public int? ContainerSize { get; set; }
        public decimal? TruckWeight { get; set; }
        public string? TypeID { get; set; }
        public string? TransporterID { get; set; }
        public Boolean? Active { get; set; }
        public Boolean? IsRGL { get; set; }
        public string? DriverLicenseNo { get; set; }
        public DateTime? LastPassedDate { get; set; }
        public string? VehicleBackRegNo { get; set; }
        public Boolean? IsBlack { get; set; }
        public DateTime? BlackDate { get; set; }
        public DateTime? BlackRemovedDate { get; set; }
        public string? BlackReason { get; set; }
        public string? BlackRemovedReason { get; set; }
        public string? Remarks { get; set; }
        public string? UpdatedUser { get; set; }
        public string? CreatedUser { get; set; }
    }
}
