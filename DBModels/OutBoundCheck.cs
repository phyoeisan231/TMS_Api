using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class OutBoundCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OutRegNo { get; set; } = 0!;
        [Column(TypeName = "varchar(25)")]
        public string? OutYardID { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? OutGateID { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? OutPCCode { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? OutContainerType { get; set; }
        [Column(TypeName = "int")]
        public int? OutContainerSize { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? OutType { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? OutCargoType { get; set; }
        [Column(TypeName = "int")]
        public int? OutNoOfContainer { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TruckType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OutCheckDateTime { get; set; }
        [Column(TypeName = "decimal(18,5)")]
        public decimal? OutBoundWeight { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TruckVehicleRegNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TrailerVehicleRegNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? DriverLicenseNo { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? DriverName { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string? JobCode { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string? JobDescription { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? CardNo { get; set; }

        [Column(TypeName = "varchar(15)")]
        public string? Status { get; set; }//Valid,Invalid
        [Column(TypeName = "varchar(max)")]
        public string? Remark { get; set; }

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
