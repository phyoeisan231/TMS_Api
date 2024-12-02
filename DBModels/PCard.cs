using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DBModels
{
    public class PCard
    {
        [Key]
        [Column(TypeName = "varchar(25)")]
        public string CardNo { get; set; } = null!;//mandatory

        [Column(TypeName = "varchar(25)")]
        public string? YardID { get; set; }//mandatory
        [Column(TypeName = "varchar(10)")]
        public string? GroupName { get; set; }//mandatory

        [Column(TypeName = "bit")]
        public Boolean? Active { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? IsUse { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? VehicleRegNo { get; set; }
        [Column(TypeName ="datetime")]
        public DateTime? CardIssueDate { get; set; }

        [Column(TypeName ="datetime")]
        public DateTime? CardReturnDate { get; set; }
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
