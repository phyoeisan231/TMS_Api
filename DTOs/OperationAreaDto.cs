﻿namespace TMS_Api.DTOs
{
    public class OperationAreaDto
    {
        public string AreaID { get; set; } = null!;//auto or not 
        public string? Name { get; set; }
        public string? YardID { get; set; }
        public Boolean? Active { get; set; }
        public Boolean? IsWaitingArea { get; set; }
        public string? GroupName { get; set; }
        public string? UpdatedUser { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
