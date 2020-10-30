using System;

namespace HelperDesk.API.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool isMain { get; set; }
        public string PublicId { get; set; }
        public Management Management { get; set; }
        public int ManagementId { get; set; }
        public Position Position { get; set; }
        public int PositionId { get; set; }
    }
}