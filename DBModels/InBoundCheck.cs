using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class InBoundCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InRegNo { get; set; } = 0!;

        [Column(TypeName = "varchar(25)")]
        public string? InYardID { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? InGateID { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? InPCCode { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? InContainerType { get; set; }
        [Column(TypeName = "int")]
        public int? InContainerSize { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? InType { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? InCargoType { get; set; }
        [Column(TypeName = "int")]
        public int? InNoOfContainer { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TruckType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InCheckDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InArrivalDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InDepartureDateTime { get; set; }
       
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
