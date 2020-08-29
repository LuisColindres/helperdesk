using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;

namespace HelperDesk.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Names { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public Gender Gender { get; set; }

        public int GenderId { get; set; }

        public Role Role { get; set; }

        public int RoleId { get; set; }

        public Company Company { get; set; }

        public int CompanyId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int status { get; set; }

        public Boolean Active { get; set; }
        public string Authy_id { get; set; }
    }
}