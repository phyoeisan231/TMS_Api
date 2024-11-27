using System.ComponentModel.DataAnnotations.Schema;

namespace TMS_Api.DTOs
{
    public class PCardDto
    {
        public string CardNo { get; set; } = null!;
        public string? YardID { get; set; }
        public string? GroupName { get; set; }
        public Boolean? Active { get; set; }
        public Boolean? IsUse { get; set; }
        public string? VehicleRegNo { get; set; }
        public DateTime? CardIssueDate { get; set; }
        public DateTime? CardReturnDate { get; set; }
        public string? UpdatedUser { get; set; }
        public string? CreatedUser { get; set; }
    }
}
