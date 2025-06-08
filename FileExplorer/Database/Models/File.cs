using System.ComponentModel.DataAnnotations;

namespace FileExplorer.Database.Models
{
    public class File
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FullPath { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public User CreatedBy { get; set; }

        public List<FileHistory> Modifications { get; set; }
    }
}
