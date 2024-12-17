
namespace TMS_Api.DTOs
{
    public class ResponseMessage
    {
        public string? MessageContent { get; set; }
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? ServiceBillNo { get; set; }
        public string? Yard { get; set; }
    }
}
