using System;
using Microsoft.AspNetCore.Http;

namespace HelperDesk.API.Dtos
{
    public class FileForUpdateDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool isMain { get; set; } 
        public int ManagementId { get; set; }
        public int PositionId { get; set; }
    }
}