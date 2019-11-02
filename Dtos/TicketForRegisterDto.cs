namespace HelperDesk.API.Dtos
{
    public class TicketForRegisterDto
    {
        public string Description { get; set; }
        public int RequestingUserId { get; set; }
        public int TicketTypeId { get; set; }
        public int TicketCategoryId { get; set; }
        public int TicketStatusId { get; set; }
    }
}