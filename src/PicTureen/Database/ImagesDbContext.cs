using System.Data.Entity;
using System.Data.SQLite;
using Interfaces;

namespace Database
{
    public class ImagesDbContext: DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }  
        public ImagesDbContext(IDbHelper dbHelper):base(new SQLiteConnection(dbHelper.ConnectionString), true)
        {
            
        }
    }
}
