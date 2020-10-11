using System;

namespace HelperDesk.API.Models
{
    public class Email
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string ForwardEmail { get; set; }
        public Management Managament { get; set; }
        public int ManagamentId { get; set; }
        public int Status { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}