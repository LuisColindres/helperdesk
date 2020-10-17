using System;

namespace HelperDesk.API.Dtos
{
    public class SessionForFilterDto
    {
        public int DepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}