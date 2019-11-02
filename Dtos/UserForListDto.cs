using System;
using HelperDesk.API.Models;

namespace HelperDesk.API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string Names { get; set; }

        public string LastName { get; set; }

        public string CompleteName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }
        
        public Gender Gender { get; set; }

        public int GenderId { get; set; }

        public Role Role { get; set; }

        public string RoleDescription { get; set; }
        public int RoleId { get; set; }

        public Company Company { get; set; }

        public int CompanyId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int Status { get; set; }
    }
}