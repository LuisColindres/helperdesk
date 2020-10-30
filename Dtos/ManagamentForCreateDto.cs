using System;
using Microsoft.AspNetCore.Http;

namespace HelperDesk.API.Dtos
{
    public class ManagamentForCreateDto
    {
        public int DepartmentId { get; set; }
        public int UserCreatedId { get; set; }
        public int AssignedUserId { get; set; }
        public string Description { get; set; }
        public string Response { get; set; }
        public IFormFile File { get; set; }
        public int Status { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}