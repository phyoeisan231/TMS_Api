using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class ICD_TruckProcess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(TypeName = "int")]
        public int? InRegNo { get; set; }
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
        public string? InContainerType { get; set; }
        [Column(TypeName = "int")]
        public int? InContainerSize { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? InContainerNo { get; set; }
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
        [ConcurrencyCheck]
        [Column(TypeName = "varchar(25)")]
        public string? CardNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TransporterID { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? TransporterName { get; set; }
       
        [Column(TypeName = "varchar(150)")]
        public string? Customer { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? InYard { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? IsUseWB { get; set; }//IsUseWeightBridge
        [Column(TypeName = "varchar(25)")]
        public string? InWBBillOption { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? OutWBBillOption { get; set; }//Credit,Cash,None
        //Out
        [Column(TypeName = "int")]
        public int? OutRegNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? OutYardID { get; set; }//mandatory
        [Column(TypeName = "varchar(25)")]
        public string? OutGateID { get; set; }//mandatory
        [Column(TypeName = "varchar(10)")]
        public string? OutPCCode { get; set; }//Category//mandatory
        
        [Column(TypeName = "varchar(25)")]
        public string? OutType { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? OutCargoType { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? OutCargoInfo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OutCheckDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InGatePassTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OutGatePassTime { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? OutContainerType { get; set; }
        [Column(TypeName = "int")]
        public int? OutContainerSize { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? OutContainerNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? InWeightBridgeID { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? OutWeightBridgeID { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InWeightDateTime { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OutWeightDateTime { get; set; }
        [ConcurrencyCheck]
        [Column(TypeName = "varchar(20)")]
        public string? Status { get; set; }//['In(Check)', 'In','In(Weight)','Out(Weight)', 'Out(Check)', 'Out'];
        [Column(TypeName = "varchar(max)")]
        public string? Remark { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? JobDept { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? JobCode { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? JobType { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string? BlNo { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? GroupName { get; set; }//mandatory"ICD","TMS","Others"
        [Column(TypeName = "int")]
        public int? PropNo { get; set; }
        [Column(TypeName = "int")]
        public int? GRNNo { get; set; }
        [Column(TypeName = "int")]
        public int? GDNNo { get; set; }
        [ConcurrencyCheck]
        [Column(TypeName = "datetime")]
        public DateTime? OptStartDate { get; set; }//OperationStart
        [ConcurrencyCheck]
        [Column(TypeName = "datetime")]
        public DateTime? OptEndDate { get; set; }//OperationEnd
        [ConcurrencyCheck]
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [ConcurrencyCheck]
        [Column(TypeName = "varchar(50)")]
        public string? UpdatedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? CreatedUser { get; set; }
    }
}
