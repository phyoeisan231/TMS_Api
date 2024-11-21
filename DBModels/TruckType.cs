using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class TruckType//Truck, Car, Van
    {
        [Key]
        [Column(TypeName = "varchar(25)")]
        public string TypeID { get; set; } = null!;//auto or not
        [Column(TypeName = "varchar(30)")]
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
