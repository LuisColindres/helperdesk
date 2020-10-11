using System;

namespace HelperDesk.API.Dtos
{
    public class ManagamentForListDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Department { get; set; }
        public int UserCreatedId { get; set; }
        public string UserCreated { get; set; }
        public int AssignedUserId { get; set; }
        public string AssignedUser { get; set; }
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