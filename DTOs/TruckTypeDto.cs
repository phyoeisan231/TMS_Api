
namespace TMS_Api.DTOs
{
    public class TruckTypeDto
    {
        public string TypeCode { get; set; } = null!;
        public string? Description { get; set; }
        public Boolean? Active { get; set; }
        public string? UpdatedUser { get; set; }
    }
}
