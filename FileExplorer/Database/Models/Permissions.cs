using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileExplorer.Database.Enums;

namespace FileExplorer.Database.Models
{
    public class Permissions
    {
        public int Id { get; set; }
        public User User { get; set; }
        public File File { get; set; }
        public Permission Permission { get; set; }
        public User GrantedBy { get; set; }
        public DateTime GrantedAt { get; set; }

    }
}
