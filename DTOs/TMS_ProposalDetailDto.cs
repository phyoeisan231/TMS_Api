using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DTOs
{
    public class TMS_ProposalDetailDto
    {

        public int PropNo { get; set; } = 0!;
        public string? TruckNo { get; set; }
        public string? TruckAssignId { get; set; }
        public string? TruckAssignOption { get; set; }
        public string? DriverName { get; set; }
        public string? DriverContact { get; set; }
        public string? NightStop { get; set; }
        public string? OtherInfo { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public string? AssignType { get; set; }
        public string? JobType { get; set; }
    }
}
