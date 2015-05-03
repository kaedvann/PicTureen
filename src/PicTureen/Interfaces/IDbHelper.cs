namespace Interfaces
{
    public interface IDbHelper
    {
        string ConnectionString { get; }
        void CreateTables();
    }
}