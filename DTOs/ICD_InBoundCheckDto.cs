namespace TMS_Api.DTOs
{
    public class ICD_InBoundCheckDto
    {
        public int InRegNo { get; set; } = 0!;//mandatory
        public string? InYardID { get; set; }//mandatory
        public string? InGateID { get; set; }//mandatory
        public string? InPCCode { get; set; }//Category//mandatory
        public string? InType { get; set; }//FCL,LCL
        public string? InCargoType { get; set; }//Laden,MT
        public string? InCargoInfo { get; set; }
        public DateTime? InCheckDateTime { get; set; }
        public string? AreaID { get; set; }
        public string? TruckType { get; set; }
        public string? TruckVehicleRegNo { get; set; }
        public string? TrailerVehicleRegNo { get; set; }
        public string? DriverLicenseNo { get; set; }
        public string? DriverContactNo { get; set; }
        public string? DriverName { get; set; }
        public string? CardNo { get; set; }
        public string? InStatus { get; set; }
        public string? TransporterID { get; set; }
        public string? TransporterName { get; set; }
        public Boolean? Status { get; set; }
        public string? Remark { get; set; }
        public string? Customer { get; set; }
        public string? UpdatedUser { get; set; }
        public string? CreatedUser { get; set; }
        public string? InWeightBridgeID { get; set; }
        public string? OutWeightBridgeID { get; set; }
        public Boolean? IsUseWB { get; set; }//IsUseWeightBridge
        public string? InWBBillOption { get; set; }//Credit,Cash,None
        public string? OutWBBillOption { get; set; }//Credit,Cash,None
        public List<ICD_InBoundCheck_DocumentDto>? DocumentList { get; set; }

    }
}
