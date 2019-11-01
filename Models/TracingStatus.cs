using System;

namespace HelperDesk.API.Models
{
    public class TracingStatus
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}