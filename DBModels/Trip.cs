using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DBModels
{
    public class Trip
    {
        [Key]
        [Column(TypeName = "varchar(15)")]
        public string TripID { get; set; } = null!;
        [Column(TypeName = "varchar(50)")]
        public string? TripType { get; set; }
        [Column(TypeName ="varchar(50)")]
        public string? LoadType {  get; set; }
        [Column(TypeName ="decimal")]
        public decimal? LoadWeight { get; set; }
        [Column(TypeName ="varchar(100)")]
        public string? DeliveryLocation {  get; set; }
        [Column(TypeName ="varchar(100)")]
        public string? ConsingeeInfo {  get; set; }
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
