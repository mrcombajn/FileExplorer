using FileExplorer.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FileExplorer.Database
{
    public class FileExplorerContext : DbContext
    {
        #region Constructors and Deconstructors

        public FileExplorerContext() { }        

        #endregion

        #region Properties

        public DbSet<User> Users => Set<User>();
        public DbSet<File> Files => Set<File>();
        public DbSet<FileHistory> FilesHistory => Set<FileHistory>();
        public DbSet<Notifications> Notifications => Set<Notifications>();
        public DbSet<Permissions> Permissions => Set<Permissions>();

        #endregion

        #region Protected Methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\Praca\\AppData\\Roaming\\FileExplorer\\fileexplorer.db");
        }

        #endregion
    }
}
