using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DTOs
{
    public class ICD_OutBoundCheckDto
    {
        public int OutRegNo { get; set; } = 0!;//mandatory
        public string? OutYardID { get; set; }//mandatory
        public string? OutGateID { get; set; }//mandatory
        public string? OutPCCode { get; set; }//Category//mandatory
        public string? OutType { get; set; }
        public string? OutCargoType { get; set; }
        public string? OutCargoInfo { get; set; }
        public DateTime? OutCheckDateTime { get; set; }
        public string? AreaID { get; set; }
        public string? TruckType { get; set; }
        public string? TruckVehicleRegNo { get; set; }
        public string? TrailerVehicleRegNo { get; set; }
        public string? DriverLicenseNo { get; set; }
        public string? DriverName { get; set; }
        public string? DriverContactNo { get; set; }
        public string? CardNo { get; set; }    
        public Boolean? OutboundWeight { get; set; }
        public string? OutWeightBridgeID { get; set; }
        public Boolean? Status { get; set; }
        public string? Remark { get; set; }
        public string? Customer { get; set; }
        public string? UpdatedUser { get; set; }
        public string? CreatedUser { get; set; }
        public int? InRegNo { get; set; }
        public string? GroupName { get; set; }//mandatory
        public List<ICD_OutBoundCheck_DocumentDto>? DocumentList { get; set; }

    }
}
