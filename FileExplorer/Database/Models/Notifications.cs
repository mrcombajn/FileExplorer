using FileExplorer.Database.Enums;

namespace FileExplorer.Database.Models
{
    public class Notifications
    {
        public int Id { get; set; }
        public User User { get; set; }
        public File File { get; set; }
        public NotificationType NotificationType { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }

    }
}
