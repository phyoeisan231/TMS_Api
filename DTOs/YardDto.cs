namespace TMS_Api.DTOs
{
    public class YardDto
    {
        public string YardID { get; set; } = null!;
        public string? Name { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }

    }
}
