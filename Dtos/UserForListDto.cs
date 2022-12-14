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

        public Position Position { get; set; }
        public int PositionId { get; set; }
        public string PositionDescription { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentDescription { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int Status { get; set; }

        public bool add { get; set; }

        public bool edit { get; set; }

        public bool delete { get; set; }

        public string Authy_id { get; set; }
    }
}