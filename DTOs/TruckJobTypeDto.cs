namespace TMS_Api.DTOs
{
    public class TruckJobTypeDto
    {
        public string TypeID { get; set; } = null!;
        public string? Description { get; set; }
        public Boolean? Active { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }

    }
}
