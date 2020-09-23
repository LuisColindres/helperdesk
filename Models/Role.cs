using System;
using System.Collections.Generic;

namespace HelperDesk.API.Models
{
    public class Role
    {
        public int Id { get; set; }

        public string RoleDescription { get; set; }

        public bool status { get; set; }

        public bool add { get; set; }

        public bool edit { get; set; }

        public bool delete { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<User> Users { get; set; }
    }
}