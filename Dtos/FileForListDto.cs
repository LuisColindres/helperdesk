using System;
using HelperDesk.API.Models;

namespace HelperDesk.API.Dtos
{
    public class FileForListDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool isMain { get; set; }
        public string PublicId { get; set; }
        public Management Management { get; set; }
        public Position Position { get; set; }
    }
}