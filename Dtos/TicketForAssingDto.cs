using System;

namespace HelperDesk.API.Dtos
{
    public class TicketForAssingDto
    {
        public int? UserAssingId { get; set; }
        public int? AssignedUserId { get; set; }
        public int? TicketStatusId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? AssignationDate { get; set; }
    }
}