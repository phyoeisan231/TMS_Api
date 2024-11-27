using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class TruckJobType//Rail,WH,ICD,Other,(Rail,WH,Truck,CCA,Store,WS for workshop,ICD,QEHS)
    {
        [Key]
        [Column(TypeName = "varchar(25)")]
        public string TypeID { get; set; } = null!;
        [Column(TypeName = "varchar(50)")]
        public string? Description { get; set; }

        [Column(TypeName = "bit")]
        public Boolean? Active { get; set; }
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
