namespace TMS_Api.DTOs
{
    public class TruckDto
    {
        public string VehicleRegNo { get; set; } = null!;
        public decimal? TruckWeight { get; set; }
        public string? Remarks { get; set; }
        public string? TruckType { get; set; }
        public string? Transporter { get; set; }
        public Boolean? IsBlack { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? BlackDate { get; set; }
        public DateTime? BlackRemovedDate { get; set; }
        public string? DriverLicenseNo { get; set; }
        public DateTime? LastPassedDate { get; set; }
        public string? VehicleBackRegNo { get; set; }
        public string? BlackReason { get; set; }
        public string? BlackRemovedReason { get; set; }
        //public string? Name { get; set; }
        //public string? ContainerType { get; set; }
        //public int? ContainerSize { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
    }
}
