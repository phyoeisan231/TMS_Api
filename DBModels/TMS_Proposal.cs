using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class TMS_Proposal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PropNo { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? Yard { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EstDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? JobDept { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? JobCode { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? JobType { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string? CompanyName { get; set; }
        [Column(TypeName = "int")]
        public int? NoOfTruck { get; set; }
        [Column(TypeName = "int")]
        public int? NoOfTEU { get; set; }
        [Column(TypeName = "int")]
        public int? NoOfFEU { get; set; }
        [Column(TypeName = "int")]
        public int? LCLQty { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string? CargoInfo { get; set; }
        [Column(TypeName = "varchar(12)")]
        public string? CustomerId { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string? CustomerName { get; set; }
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
