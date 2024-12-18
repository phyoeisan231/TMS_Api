using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace TMS_Api.DTOs
{
    public class TMS_ProposalDto
    {
        public int PropNo { get; set; }
        public string? Yard { get; set; }
        public DateTime? EstDate { get; set; }
        public string? JobCode { get; set; }
        public string? JobDept { get; set; }
        public string? JobType { get; set; }
        public string? CompanyName { get; set; }
        public int? NoOfTruck { get; set; }
        public int? NoOfTEU { get; set; }
        public int? NoOfFEU { get; set; }
        public int? LCLQty { get; set; }
        public string? CargoInfo { get; set; }
        public string? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? UpdatedUser { get; set; }
        public string? CreatedUser { get; set; }
        public List<TMS_ProposalDetailDto>? DetailList { get; set; }
        public DataTable? ProposalDetailList { get; set; }
        
    }
}
