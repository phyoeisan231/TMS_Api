﻿namespace TMS_Api.DTOs
{
    public class GateDto
    {
        public string GateID { get; set; } = null!;
        public string? Name { get; set; }
        public string? YardID { get; set; }
        public string? Type { get; set; }//InBound,OutBound,Both
        public Boolean? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedUser { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedUser { get; set; }

    }
}
