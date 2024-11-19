using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DBModels
{
    public class WeightBridge
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string WeightBridgeID { get; set; } = null!;

        [Column(TypeName ="datetime")]
        public DateTime? DateTime { get; set; }

        [Column(TypeName ="varchar(25)")]
        public string? VehicleNo {  get; set; }

        [Column(TypeName ="varchar(50)")]
        public string? DriverName {  get; set; }

        [Column(TypeName ="decimal")]
        public decimal? GrossWeight { get; set; }

        [Column(TypeName ="decimal")]
        public decimal? TareWeight { get; set; }

        [Column(TypeName ="decimal")]
        public decimal? NetWeight { get; set; }

        [Column(TypeName ="varchar(15)")]
        public string? WBOperator { get; set; }

        [Column(TypeName ="varchar(20)")]
        public string? WBTicketNo {  get; set; }

        [Column(TypeName ="varchar(50)")]
        public string? Commodity {  get; set; }

        [Column(TypeName ="varchar(15)")]
        public string? TripID { get; set; }

        [Column(TypeName ="varchar(max)")]
        public string? Remarks {  get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? UpdatedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? CreatedUser { get; set; }

    }
}
