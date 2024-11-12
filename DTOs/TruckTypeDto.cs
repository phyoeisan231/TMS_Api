
namespace TMS_Api.DTOs
{
    public class TruckTypeDto
    {
        public int TypeCode { get; set; } = 0!;
        public string? Description { get; set; }
        public Boolean? Active { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
