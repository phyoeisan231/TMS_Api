using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class Truck
    {
        [Key]
        [Column(TypeName = "varchar(25)")]
        public string VehicleRegNo { get; set; } = null!;
        [Column(TypeName = "Decimal")]
        public decimal? UnladenWeight { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? Remarks { get; set; }
        [Column(TypeName = "float")]
        public float? Flowrate { get; set; }
        [Column(TypeName = "varchar(30)")]
        public string? TruckType { get; set; }
        [Column(TypeName = "varchar(10)")]
        public string? Transporter { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? IsBlack { get; set; }
        [Column(TypeName = "bit")]
        public Boolean? Active { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? LoadingType { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BlackDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? BlackRemovedDate { get; set; }

        [Column(TypeName = "varchar(25)")]
        public string? DriverLicenseNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastLoadingDate { get; set; }
        [Column(TypeName = "varchar(25)")]
        public string? VehicleBackRegNo { get; set; }

        [Column(TypeName = "int")]
        public int TotalNrDomeCover { get; set; }
        [Column(TypeName = "int")]
        public int TotalNrLoadCoupling { get; set; }
        [Column(TypeName = "int")]
        public int TotalNrFootValve { get; set; }
        [Column(TypeName = "int")]
        public int TotalNrCabinet { get; set; }
        [Column(TypeName = "varchar(50)")]
        public string? OtherSealPlace { get; set; }

        [Column(TypeName = "varchar(max)")]
        public string? BlackReason { get; set; }
        [Column(TypeName = "varchar(max)")]
        public string? BlackRemovedReason { get; set; }
        [Column(TypeName = "int")]
        public int? CompartmentNo { get; set; }
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
