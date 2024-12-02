using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DBModels
{
    public class DocumentSetting
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string DocCode { get; set; } = null!;//mandatory
        [Column(TypeName ="varchar(50)")]
        public string? DocName {  get; set; }//mandatory
        [Column(TypeName = "varchar(10)")]
        public string? PCCode { get; set; }//mandatory
        [Column(TypeName ="bit")]
        public Boolean? AttachRequired { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? IsInDoc { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? IsOutDoc { get; set; }
        [Column(TypeName ="bit")]
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
