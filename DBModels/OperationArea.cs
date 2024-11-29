using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class OperationArea
    {
        [Key]
        [Column(TypeName = "varchar(25)")]
        public string AreaID { get; set; } = null!;//mandatory
        [Column(TypeName = "varchar(30)")]
        public string? Name { get; set; }//mandatory
        [Column(TypeName = "varchar(25)")]
        public string? YardID { get; set; }//mandatory

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
