using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DTOs
{
    public class WeightServiceBillDto
    {
        public string ServiceBillNo { get; set; } = null!;
        public string? WeightBridgeID { get; set; }
        public DateTime? ServiceBillDate { get; set; }
        public string? TruckNo { get; set; }
        public string? TrailerNo { get; set; }
        public int? QRegNo { get; set; }
        public decimal? Weight { get; set; }
        public string? WeightType { get; set; }
        public string? CargoInfo { get; set; }
        public string? WeightOption { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public decimal? CashAmt { get; set; }
        public int? CheckInRegNo { get; set; }
        public string? WeightCategory { get; set; }
        public string? DriverName { get; set; }
        public string? DriverLicense { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
