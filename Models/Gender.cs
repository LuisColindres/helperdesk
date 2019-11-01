using System;
using System.Collections.Generic;

namespace HelperDesk.API.Models
{
    public class Gender
    {
        public int Id { get; set; }

        public string description { get; set; }

        public int status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<User> Users { get; set; }
    }
}