using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DBModels
{
    public class Trip1
    {
        [Key]
        [Column(TypeName = "varchar(15)")]
        public string TripID { get; set; } = null!;
        [Column(TypeName = "varchar(50)")]
        public string? TripType { get; set; }
        [Column(TypeName ="varchar(50)")]
        public string? LoadType {  get; set; }
        [Column(TypeName ="decimal(18,5)")]
        public decimal? LoadWeight { get; set; }
        [Column(TypeName ="varchar(100)")]
        public string? DeliveryLocation {  get; set; }
        [Column(TypeName ="varchar(100)")]
        public string? ConsigneeInfo {  get; set; }
        [Column(TypeName ="varchar(25)")]
        public string? BLNo { get; set; }
        [Column(TypeName ="varchar(25)")]
        public string? DONo {  get; set; }
        [Column(TypeName ="decimal")]
        public string? Price {  get; set; }
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
