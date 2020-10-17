using System;

namespace HelperDesk.API.Dtos
{
    public class EmailForFilterDto
    {
        public int DepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}