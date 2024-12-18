using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class WeightBridgeQueue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RegNo { get; set; } = 0!;//mandatory
        [Column(TypeName = "int")]
        public int? QueueNo { get; set; } //increase no depend on InRegNo And WeighID
        [Column(TypeName = "int")]
        public int? InRegNo { get; set; }//mandatory
        [Column(TypeName = "varchar(25)")]
        public string? YardID { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? GateID { get; set; }//mandatory
       
        [Column(TypeName = "varchar(15)")]
        public string? Type { get; set; }//In,Out/Single
        [Column(TypeName = "varchar(25)")]
        public string? CargoType { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? CargoInfo { get; set; }

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
        public string? WeightBridgeID { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? WBOption { get; set; }//Single,Both
        [Column(TypeName = "varchar(25)")]
        public string? BillOption { get; set; }//Credit,Foc,Cash,None
        [Column(TypeName = "int")]
        public int? WBillNo { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string? Customer { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? JobCode { get; set; }
        [Column(TypeName = "varchar(150)")]
        public string? JobDescription { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string? BlNo { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? ContainerNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? WeightDateTime { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? Status { get; set; }//Queue,Done
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
