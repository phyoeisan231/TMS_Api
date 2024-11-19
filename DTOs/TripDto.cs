namespace TMS_Api.DTOs
{
    public class TripDto
    {
        public string TripID { get; set; } = null!;
        public string? TripType { get; set; }
        public string? LoadType { get; set; }
        public decimal? LoadWeight { get; set; }
        public string? DeliveryLocation { get; set; }
        public string? ConsingeeInfo { get; set; }
        public string? BLNo { get; set; }
        public string? DONo { get; set; }
        public string? Price { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
    }
}
