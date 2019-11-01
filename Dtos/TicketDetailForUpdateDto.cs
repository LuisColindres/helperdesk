using System;

namespace HelperDesk.API.Dtos
{
    public class TicketDetailForUpdateDto
    {
        public string Description { get; set; }
        public string Archive { get; set; }
        public int TicketId { get; set; }
        public int TracingStatusId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}