using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DBModels
{
    public class Gate
    {
        [Key]
        [Column(TypeName = "varchar(25)")]//YTGGate,MHOGate,MNPA1MNPA2//mandatory
        public string GateID { get; set; } = null!;
        [Column(TypeName = "varchar(30)")]
        public string? Name { get; set; }//mandatory
        [Column(TypeName = "varchar(25)")]
        public string? YardID { get; set; }//mandatory
        [Column(TypeName = "varchar(25)")]
        public string? Type { get; set; }//InBound,OutBound,Both//mandatory
        [Column(TypeName = "bit")]
        public Boolean? Active { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string? Phone { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? Email { get; set; }
        [Column(TypeName = "varchar(250)")]
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
