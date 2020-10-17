using System;
using HelperDesk.API.Models;

namespace HelperDesk.API.Dtos
{
    public class SessionForListDto
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Names { get; set; }
        public string LastName { get; set; }
        public string PositionDescription { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentDescription { get; set; }
        public string Information { get; set; }
        public int Attempts { get; set; }
        public int Active { get; set; }
        public DateTime LoginAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}