using Interfaces;

namespace Database
{
    public class DbHelper : IDbHelper
    {
        public string ConnectionString { get { return "DataSource = test.db; version = 3"; } }
    }
}
