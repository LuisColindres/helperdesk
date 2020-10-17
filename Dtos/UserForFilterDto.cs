using System;

namespace HelperDesk.API.Dtos
{
    public class UserForFilterDto
    {
        public int DepartmentId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}