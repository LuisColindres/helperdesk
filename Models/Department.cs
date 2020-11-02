using System;
using System.Collections.Generic;

namespace HelperDesk.API.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool add { get; set; }
        public bool edit { get; set; }
        public bool delete { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}