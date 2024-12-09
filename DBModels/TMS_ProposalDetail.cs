using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class TMS_ProposalDetail
    {
        [Key]
        [Column(TypeName = "int")]
        public int PropNo { get; set; } = 0!;
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string? TruckNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TruckAssignId { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string? TruckAssignOption { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? DriverName { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? DriverContact { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? NightStop { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string? OtherInfo { get; set; }
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
