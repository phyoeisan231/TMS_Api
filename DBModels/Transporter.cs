using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class Transporter
    {
        [Key]
        [Column(TypeName = "varchar(25)")]
        public string TransporterID { get; set; } = null!;
        [Column(TypeName = "varchar(100)")]
        public string? TransporterName { get; set; }
        [Column(TypeName = "varchar(250)")]
        public string? Address { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? ContactNo { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? Email { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? ContactPerson { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TypeID { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? Active { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? IsBlack { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BlackDate { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? BlackReason { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? BlackRemovedReason { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BlackRemovedDate { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? Remarks { get; set; }   
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? UpdatedUser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? CreatedUser { get; set; }
        [Column(TypeName = "varchar(25")]
        public string? SAPID { get; set; }
    }
}
