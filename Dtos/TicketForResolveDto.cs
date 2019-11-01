using System;
using HelperDesk.API.Models;

namespace HelperDesk.API.Dtos
{
    public class TicketForResolveDto
    {
        public string Description { get; set; }
        public User RequestingUser { get; set; }
        public int RequestingUserId { get; set; }
        public TicketType TicketType { get; set; }
        public int TicketTypeId { get; set; }
        public TicketCategory TicketCategory { get; set; }
        public int TicketCategoryId { get; set; }
        public User UserAssing { get; set; }
        public int UserAssingId { get; set; }
        public User AssignedUser { get; set; }
        public int AssignedUserId { get; set; }
        public DateTime AssignationDate { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public int TicketStatusId { get; set; }
        public DateTime UpdatedAt { get; set; }
        public TicketDetail TicketDetail { get; set; }
        public TicketDetail TicketDetailSolution { get; set; }
    }
}