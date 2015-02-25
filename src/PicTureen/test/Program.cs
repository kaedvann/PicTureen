using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Database;

namespace test
{
    class Program
    {

        static void CreateTables()
        {
            using (var conn = new SQLiteConnection(new DbHelper().ConnectionString))
            {
                conn.Open();
                const string tagTable = @"create table  if not exists tags
                                          (
                                            Id integer primary key not null,
                                            Name text)";
                const string imageTable = @"create table  if not exists images
                                          (
                                            Id integer primary key not null,
                                            Path text)";
                const string mappingTable = @"create table  if not exists ImagesTags
                                              (
                                                 tag_id integer not null,
                                                 image_id integer not null,
                                                 primary key (tag_id, image_id),
                                                 foreign key(tag_id) references tags(id),
                                                 foreign key(image_id) references images(id))";
                new SQLiteCommand(tagTable, conn).ExecuteNonQuery();
                new SQLiteCommand(imageTable, conn).ExecuteNonQuery();
                new SQLiteCommand(mappingTable, conn).ExecuteNonQuery();
            }
        }
        
        static void Main(string[] args)
        {
            CreateTables();
            using (var db = new ImagesDbContext(new DbHelper()))
            {
                db.Images.Add(new Image() {Tags = new HashSet<Tag> {new Tag(){Name = "test1"}, new Tag(){Name = "test2"}}});
                db.SaveChanges();
                foreach (var tag in db.Tags)
                {
                    Console.WriteLine(tag.Name);
                }
                Console.WriteLine("It works!");
            }
            using (var db = new ImagesDbContext(new DbHelper()))
            {
                foreach (var tag in db.Tags)
                {
                    Console.WriteLine(tag.Name);
                }
                Console.WriteLine("It works!");
            }
            Console.ReadLine();
        }
    }
}
