using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DBModels
{
    public class Trailer
    {
        [Key]
        [Column(TypeName = "varchar(25)")]
        public string VehicleRegNo { get; set; } = null!;//mandatory
        [Column(TypeName = "varchar(25)")]
        public string? ContainerType { get; set; }
        [Column(TypeName = "int")]
        public int? ContainerSize { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? TransporterID { get; set; }
        [Column(TypeName = "Decimal(18,5)")]
        public decimal? TrailerWeight { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? Remarks { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? IsBlack { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? Active { get; set; }
       
        [Column(TypeName = "varchar(25)")]
        public string? DriverLicenseNo { get; set; }
 
        [Column(TypeName = "datetime")]
        public DateTime? LastPassedDate { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? VehicleBackRegNo { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? BlackDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BlackRemovedDate { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? BlackReason { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? BlackRemovedReason { get; set; }
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
