using Npgsql;

namespace csharp_dao.Database
{
    public class DatabaseManager
    {
        public string Host { get; set; } = "localhost";

        public string Username { get; set; } = "postgres";

        public string Password { get; set; } = "root";

        public string Database { get; set; } = "Simplon";

        public NpgsqlConnection? Connection { get; set; }

        public void OpenConnection()
        {
            NpgsqlDataSourceBuilder dataSourceBuilder = new NpgsqlDataSourceBuilder($"host={Host};username={Username};password={Password};database={Database};");
            NpgsqlDataSource dataSource = dataSourceBuilder.Build();

            try
            {
                Connection = dataSource.OpenConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Connection = null;
            }
        }
    }
}
