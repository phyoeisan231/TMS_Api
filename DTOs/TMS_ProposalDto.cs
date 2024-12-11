using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TMS_Api.DTOs
{
    public class TMS_ProposalDto
    {
        public int PropNo { get; set; }= 0!;
        public string? Yard { get; set; }
        public DateTime? EstDate { get; set; }
        public string? JobCode { get; set; }
        public string? JobDescription { get; set; }
        public string? JobType { get; set; }
        public string? CompanyName { get; set; }
        public int? NoOfTruck { get; set; }
        public int? NoOfTEU { get; set; }
        public int? NoOfFEU { get; set; }
        public int? LCLQty { get; set; }
        public string? CargoInfo { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
    }
}
