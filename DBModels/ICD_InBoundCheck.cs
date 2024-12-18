using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace TMS_Api.DBModels
{
    public class ICD_InBoundCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InRegNo { get; set; } = 0!;//mandatory     
        [Column(TypeName = "varchar(25)")]
        public string? InYardID { get; set; }//mandatory
        [Column(TypeName = "varchar(25)")]
        public string? InGateID { get; set; }//mandatory
        [Column(TypeName = "varchar(10)")]
        public string? InPCCode { get; set; }//Category//mandatory     
        [Column(TypeName = "varchar(25)")]
        public string? InType { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? InCargoType { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? InCargoInfo { get; set; }     
        [Column(TypeName = "datetime")]
        public DateTime? InCheckDateTime { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? AreaID { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TruckType { get; set; }      
        [Column(TypeName = "varchar(25)")]
        public string? TruckVehicleRegNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TrailerVehicleRegNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? DriverLicenseNo { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? DriverName { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? DriverContactNo { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string? CardNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TransporterID { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? TransporterName { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? InWeightBridgeID { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? OutWeightBridgeID { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? IsUseWB { get; set; }//IsUseWeightBridge
        [Column(TypeName = "varchar(25)")]
        public string? InWBBillOption { get; set; }//Credit,Cash,None
        [Column(TypeName = "varchar(25)")]
        public string? OutWBBillOption { get; set; }//Credit,Cash,None
        [Column(TypeName = "bit")]
        public Boolean? Status { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? Remark { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string? Customer { get; set; }
        [Column(TypeName = "int")]
        public int? PropNo { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? GroupName { get; set; }//mandatory
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
