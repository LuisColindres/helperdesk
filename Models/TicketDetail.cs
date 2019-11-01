using System;

namespace HelperDesk.API.Models
{
    public class TicketDetail
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Archive { get; set; }
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public TracingStatus TracingStatus { get; set; }
        public int TracingStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}