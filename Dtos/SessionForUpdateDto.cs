using System;
using HelperDesk.API.Models;

namespace HelperDesk.API.Dtos
{
    public class SessionForUpdateDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Information { get; set; }
        public int Attempts { get; set; }
        public int Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}