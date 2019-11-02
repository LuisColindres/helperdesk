using System;

namespace HelperDesk.API.Dtos
{
    public class TicketForListDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string RequestingUserName { get; set; }
        public int RequestingUserId { get; set; }
        public string TicketTypeDescription { get; set; }
        public int TicketTypeId { get; set; }
        public string TicketCategoryDescription { get; set; }
        public int TicketCategoryId { get; set; }
        public string UserAssingName { get; set; }
        public int UserAssingId { get; set; }
        public string AssignedUserName { get; set; }
        public int AssignedUserId { get; set; }
        public DateTime AssignationDate { get; set; }
        public string TicketStatusDescription { get; set; }
        public int TicketStatusId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}