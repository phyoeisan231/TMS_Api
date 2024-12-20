namespace TMS_Api.DTOs
{
    public class FileUploadDTO
    {
        public string? JobDept { get; set; }
        public IFormFile? UploadedFile { get; set; }

    }
}
