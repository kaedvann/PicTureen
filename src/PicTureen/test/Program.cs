using System;
using System.Data.SQLite;
using Database;

namespace test
{
    class Program
    {

        static void CreateTables()
        {
            using (var conn = new SQLiteConnection(new Helper().ConnectionString))
            {
                conn.Open();
                const string tagTable = @"create table tags (Id bigint primary key, Name text)";
                new SQLiteCommand(tagTable, conn).ExecuteNonQuery();
            }
        }

        class Helper : IDbHelper
        {
            public string ConnectionString { get { return "DataSource = test.db; version = 3"; } }
        }
        static void Main(string[] args)
        {
            CreateTables();
            using (var db = new ImagesDbContext(new Helper()))
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
