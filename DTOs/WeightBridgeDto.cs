namespace TMS_Api.DTOs
{
    public class WeightBridgeDto
    {
        public string WeightBridgeID { get; set; } = null!;
        public DateTime? DateTime { get; set; }
        public string? VehicleNo { get; set; }
        public string? DriverName { get; set; }
        public decimal? GrossWeight { get; set; }
        public decimal? TareWeight { get; set; }
        public decimal? NetWeight { get; set; }
        public string? WBOperator { get; set; }
        public string? WBTicketNo { get; set; }
        public string? Commodity { get; set; }
        public string? TripID { get; set; }
        public string? Remarks { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }


    }
}
