﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DBModels
{
    public class TruckType//TR for truck, VC for Van 
    {
        [Key]
        [Column(TypeName = "varchar(25)")]
        public string TypeID { get; set; } = null!;//mandatory
        [Column(TypeName = "varchar(30)")]
        public string? Description { get; set; }//mandatory
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
