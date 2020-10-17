using System;
using HelperDesk.API.Models;

namespace HelperDesk.API.Dtos
{
    public class EmailForListDto
    {
        public int Id { get; set; }
        public int Type { get; set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string ForwardEmail { get; set; }
        public int ManagamentId { get; set; }
        public Department Department { get; set; }
        public string DepartmentDescription { get; set; }
        public int Status { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}