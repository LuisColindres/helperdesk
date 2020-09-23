using System;

namespace HelperDesk.API.Dtos
{
    public class PositionForUpdateDto
    {
        public string Name { get; set; }
        public int DepartmentId { get; set; }
        public int UserReportedId { get; set; }
        public string Target { get; set; }
        public string OrganizationalPosition { get; set; }
        public string Scope { get; set; }
        public string Dimension { get; set; }
        public string Education { get; set; }
        public string Experience { get; set; }
        public string Skills { get; set; }
        public string OtherSkills { get; set; }
        public DateTime StartDateSkills { get; set; }
        public DateTime EndDateSkills { get; set; }
        public DateTime StartDatePerfomance { get; set; }
        public DateTime EndDatePerfomance { get; set; }
        public int Status { get; set; }
        public int UpdatedByUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}