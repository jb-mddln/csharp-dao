using csharp_dao.Database;
using csharp_dao.WebApi;

namespace csharp_dao
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.OpenConnection();

            WebServerManager webServerManager = new WebServerManager();
            webServerManager.Start();

            while (true)
            {
                webServerManager.HandleRequestRoute(databaseManager.Connection);
            }
        }
    }
}