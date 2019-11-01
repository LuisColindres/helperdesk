using System;
using HelperDesk.API.Models;

namespace HelperDesk.API.Dtos
{
    public class TicketForUpdateDto
    {
        public string Description { get; set; }
        public int RequestingUserId { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketCategoryId { get; set; }
        public int UserAssingId { get; set; }
        public DateTime AssignationDate { get; set; }
        public int TicketStatusId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}