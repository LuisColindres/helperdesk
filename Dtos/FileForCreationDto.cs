using System;
using Microsoft.AspNetCore.Http;

namespace HelperDesk.API.Dtos
{
    public class FileForCreationDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }
        public bool isMain { get; set; }

        public FileForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}