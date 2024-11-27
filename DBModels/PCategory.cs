using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DBModels
{
    public class PCategory
    {
        [Key]
        [Column(TypeName = "varchar(10)")]
        public string PCCode { get; set; } = null!;

        [Column(TypeName ="varchar(50)")]
        public string? CategoryName { get; set; }

        [Column(TypeName ="decimal(18,5)")]
        public decimal? InboundWeight {  get; set; }

        [Column(TypeName ="decimal(18,5)")]
        public decimal? OutboundWeight { get; set; }

        [Column(TypeName ="varchar(10)")]
        public string? GroupName {  get; set; }

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
