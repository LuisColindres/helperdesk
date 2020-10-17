using System;

namespace HelperDesk.API.Dtos
{
    public class SessionForUpdateDto
    {
        public int UserId { get; set; }
        public string Information { get; set; }
        public int Attempts { get; set; }
        public int Active { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}