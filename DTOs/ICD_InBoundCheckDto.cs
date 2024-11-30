
namespace TMS_Api.DTOs
{
    public class ICD_InBoundCheckDto
    {
        public int InRegNo { get; set; } = 0!;
        public string? InYardID { get; set; }
        public string? InGateID { get; set; }
        public string? InPCCode { get; set; }//Category
        public string? InContainerType { get; set; }
        public int? InContainerSize { get; set; }
        public string? InType { get; set; }
        public string? InCargoType { get; set; }
        public string? InCargoInfo { get; set; }
        public int? InNoOfContainer { get; set; }
        public string? AreaID { get; set; }
        public DateTime? InCheckDateTime { get; set; }
        public string? TruckType { get; set; }
        public string? TransporterID { get; set; }
        public string? TransporterName { get; set; }
        public string? TruckVehicleRegNo { get; set; }
        public string? TrailerVehicleRegNo { get; set; }
        public string? DriverLicenseNo { get; set; }
        public string? DriverName { get; set; }
        public string? JobCode { get; set; }
        public string? JobDescription { get; set; }
        public string? CardNo { get; set; }
        public Boolean? Status { get; set; }
        public string? Remark { get; set; }
        public string? UpdatedUser { get; set; }
        public string? CreatedUser { get; set; }
        public List<ICD_InBoundCheck_DocumentDto>? DocumentList { get; set; }

    }
}
