using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DTOs
{
    public class ICD_InBoundCheckDto
    {
        public int InRegNo { get; set; } = 0!;//mandatory
        public string? InYardID { get; set; }//mandatory
        public string? InGateID { get; set; }//mandatory
        public string? InPCCode { get; set; }//Category//mandatory
        //public string? InContainerType { get; set; }//["DV","FR","GP", "HC", "HQ","HG","OS","OT","PF","RF","RH","TK", "IC", "FL", "BC", "HT", "VC", "PL"];
        //public int? InContainerSize { get; set; }// ["20", "40", "45"];
        public string? InType { get; set; }//FCL,LCL
        public string? InCargoType { get; set; }//Laden,MT
        public string? InCargoInfo { get; set; }
        //public int? InNoOfContainer { get; set; }
        public DateTime? InCheckDateTime { get; set; }
        public string? AreaID { get; set; }
        public string? TruckType { get; set; }
        public string? TruckVehicleRegNo { get; set; }
        public string? TrailerVehicleRegNo { get; set; }
        public string? DriverLicenseNo { get; set; }
        public string? DriverContactNo { get; set; }
        public string? DriverName { get; set; }
        //public string? JobCode { get; set; }
        //public string? JobDescription { get; set; }
        public string? CardNo { get; set; }
        public string? InStatus { get; set; }
        public string? TransporterID { get; set; }
        public string? TransporterName { get; set; }
        public Boolean? Status { get; set; }
        public string? Remark { get; set; }
        public string? Customer { get; set; }
        public string? UpdatedUser { get; set; }
        public string? CreatedUser { get; set; }
        public Boolean? InboundWeight { get; set; }
        public Boolean? OutboundWeight { get; set; }
        public string? InWeightBridgeID { get; set; }
        public string? OutWeightBridgeID { get; set; }
        public string? InWBOption { get; set; }//Credit,Cash,None
        public string? OutWBOption { get; set; }//Credit,Cash,None
        public List<ICD_InBoundCheck_DocumentDto>? DocumentList { get; set; }

    }
}
