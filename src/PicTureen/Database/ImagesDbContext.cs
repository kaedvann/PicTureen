using System;
using System.Data.Entity;
using System.Data.SQLite;

namespace Database
{
    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }
    public class ImagesDbContext: DbContext
    {
        public DbSet<Tag> Tags { get; set; }  
        public ImagesDbContext(IDbHelper dbHelper):base(new SQLiteConnection(dbHelper.ConnectionString), true)
        {
            
        }
    }

    public interface IDbHelper
    {
        string ConnectionString { get; }
    }
}
