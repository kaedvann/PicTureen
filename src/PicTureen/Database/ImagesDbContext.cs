using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using Interfaces;

namespace Database
{
    public class ImagesDbContext: DbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Image> Images { get; set; }  
        public ImagesDbContext(IDbHelper dbHelper):this(new SQLiteConnection(dbHelper.ConnectionString))
        {
            
        }

        internal ImagesDbContext(SQLiteConnection connection, bool contextOwnsConnection = true)
            : base(connection, contextOwnsConnection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
            new SQLiteCommand(@"pragma foreign_keys = ON", connection).ExecuteNonQuery();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().ToTable("images").Property(i => i.Path).HasColumnName("path");
            modelBuilder.Entity<Image>().HasKey(i => i.Id).Property(i => i.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Tag>().ToTable("tags");
            modelBuilder.Entity<Tag>().HasKey(t => t.Id).Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Image>().HasMany(i => i.Tags).WithMany(t => t.Images).Map(x =>
            {
                x.ToTable("ImagesTags");
                x.MapLeftKey("image_id");
                x.MapRightKey("tag_id");
            });
        }
    }
}
