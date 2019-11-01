using System;
using HelperDesk.API.Models;

namespace HelperDesk.API.Dtos
{
    public class TicketDetailDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
        public TracingStatus TracingStatus { get; set; }
        public int TracingStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}