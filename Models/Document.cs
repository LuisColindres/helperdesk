using System;
using System.Collections.Generic;

namespace HelperDesk.API.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public String Description { get; set; }
        public String Comment { get; set; }
        public String file { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
        public int Status { get; set; }
        public int CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UpdatedByUserId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}