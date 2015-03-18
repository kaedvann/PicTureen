using System.Data.SQLite;
using Interfaces;

namespace Database
{
    public class DbHelper : IDbHelper
    {
        private string _connectionString = "DataSource = test.db; foreign_keys=true; version = 3";

        public string ConnectionString
        {
            get { return _connectionString; }
            internal set { _connectionString = value; }
        }

        public void CreateTables()
        {
            using (var conn = new SQLiteConnection(ConnectionString))
            {
                CreateTables(conn);
            }
        }

        internal void CreateTables(SQLiteConnection conn)
        {
            conn.Open();
            const string tagTable = @"create table  if not exists tags
                                          (
                                            Id integer primary key not null,
                                            Name text not null)";
            const string imageTable = @"create table  if not exists images
                                          (
                                            Id integer primary key not null,
                                            Path text not null,
                                            Rating integer,
                                            Height integer,
                                            Width integer,
                                            Description text)";
            const string mappingTable = @"create table  if not exists ImagesTags
                                              (
                                                 tag_id integer not null,
                                                 image_id integer not null,
                                                 primary key (tag_id, image_id),
                                                 foreign key(tag_id) references tags(id) on delete cascade,
                                                 foreign key(image_id) references images(id) on delete cascade)";
            new SQLiteCommand(tagTable, conn).ExecuteNonQuery();
            new SQLiteCommand(imageTable, conn).ExecuteNonQuery();
            new SQLiteCommand(mappingTable, conn).ExecuteNonQuery();
            //conn.Close();
        }
    }
}
