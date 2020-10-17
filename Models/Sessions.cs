using System;

namespace HelperDesk.API.Models
{
    public class Sessions
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public string Information { get; set; }
        public int Attempts { get; set; }
        public int Active { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Sessions() {

        }

        public Sessions(int UserId, string Information, int Active, DateTime CreatedAt){
            this.UserId = UserId;
            this.Information = Information;
            this.Active = Active;
            this.CreatedAt = CreatedAt;
        }

    }
}