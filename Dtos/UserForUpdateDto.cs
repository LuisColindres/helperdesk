using System;

namespace HelperDesk.API.Dtos
{
    public class UserForUpdateDto
    {
        public string Names { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }
        public int GenderId { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int Status { get; set; }
    }
}