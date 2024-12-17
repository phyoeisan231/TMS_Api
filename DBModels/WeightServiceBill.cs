using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class WeightServiceBill
    {
        [Key]
        [Column(TypeName = "varchar(20)")]
        public string ServiceBillNo { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime? ServiceBillDate { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? WeightBridgeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InWeightTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OutWeightTime { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TruckNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TrailerNo { get; set; }
        [Column (TypeName ="int")]
        public int? QRegNo { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? InWeight { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? OutWeight { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? NetWeight { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? WeightType { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string? CargoInfo { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? WeightOption { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string? BillOption { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? CustomerId { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? CustomerName { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? CashAmt { get; set; }
        [Column(TypeName = "int")]
        public int? CheckInRegNo { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? WeightCategory { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string? DriverName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? DriverLicense { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string? ContainerNo { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? DONo { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? BLNo { get; set; }
        [Column(TypeName = "varchar(20)")]
        public string? VesselName { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TransporterID { get; set; }

        [Column(TypeName = "varchar(25)")]//YTG,MN,MHO
        public string? YardID { get; set; }
        [Column(TypeName = "varchar(25)")]//YTGGate,MHOGate,MNPA1MNPA2
        public string? GateID { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string? Remark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? CreatedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? UpdatedUser { get; set; }

    }
}
