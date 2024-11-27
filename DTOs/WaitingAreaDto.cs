namespace TMS_Api.DTOs
{
    public class WaitingAreaDto
    {
        public string AreaID { get; set; } = null!;//auto or not 
        public string? Name { get; set; }
        public string? YardID { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }

    }
}
