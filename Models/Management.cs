using System;

namespace HelperDesk.API.Models
{
    public class Management
    {
        public int Id { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public User UserCreated { get; set; }
        public int UserCreatedId { get; set; }
        public User AssignedUser { get; set; }
        public int AssignedUserId { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public string Response { get; set; }
        public int Status { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedByUserId { get; set; }
        public DateTime UpdatedAt { get; set; }

    }
}