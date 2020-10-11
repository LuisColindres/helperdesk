using System;

namespace HelperDesk.API.Dtos
{
    public class ManagamentForUpdateDto
    {
        public int DepartmentId { get; set; }
        public int UserCreatedId { get; set; }
        public int AssignedUserId { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public string Response { get; set; }
        public int Status { get; set; }
        public int UpdatedByUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}