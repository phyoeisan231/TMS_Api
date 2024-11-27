using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class OutBoundCheckDocument
    {
        [Key]
        [Column(TypeName = "int")]
        public int OutRegNo { get; set; } = 0!;
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string? DocCode { get; set; }
        [Column(TypeName = "varchar(15)")]
        public string? CheckStatus { get; set; }//Valid,Invalid
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
