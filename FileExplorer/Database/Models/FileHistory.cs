namespace FileExplorer.Database.Models
{
    public class FileHistory
    {
        public int Id { get; set; }
        public File File { get; set; }
        public User ModifiedBy { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string ModificationNote { get; set; }

    }
}
