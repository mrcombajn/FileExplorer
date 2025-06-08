using System.Net;

namespace FileExplorer.Database.Models
{
    public class User
    {

        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public IPAddress IPAddress { get; set; }

    }
}
