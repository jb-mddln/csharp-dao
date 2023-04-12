using csharp_dao.Database.Tables;
using Npgsql;

namespace csharp_dao.Database.Repository
{
    public class DogRepository
    {
        public NpgsqlConnection? Connection { get; set; }
        
        public DogRepository(NpgsqlConnection connection)
        {
            Connection = connection;
        }

        public List<Dog> FindAll(int limit = -1)
        {
            List<Dog> dogs = new List<Dog>();

            if (Connection == null)
                return dogs;

            string sql = "select * from dogs";
            if (limit > 0)
                sql += $" limit {limit}";

            using (var cmd = new NpgsqlCommand(sql, Connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dog dog = new Dog();
                        dog.Id = (int)reader["id"];
                        dog.Name = (string)reader["name"];
                        dog.Breed = (string)reader["breed"];
                        dog.Birthdate = reader["birth_date"] is DBNull ? null : (DateTime)reader["birth_date"];
                        dogs.Add(dog);
                    }
                }
            }

            return dogs;
        }

        public void Save(Dog dog)
        {
            if (Connection == null)
                return;

            if (dog.Id < 0)
                return;

            using (var cmd = new NpgsqlCommand("INSERT INTO dogs (id, name, breed, birth_date) VALUES (@id, @name, @breed, @birth_date)", Connection))
            {
                cmd.Parameters.AddWithValue("id", dog.Id);
                cmd.Parameters.AddWithValue("name", dog.Name);
                cmd.Parameters.AddWithValue("breed", dog.Breed);
                cmd.Parameters.AddWithValue("birth_date", dog.Birthdate.HasValue ? dog.Birthdate : DBNull.Value);
                
                int rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected > 0)
                {
                    Console.WriteLine($"Dog {dog.Id} {dog.Name} saved in database");
                }
                else
                {
                    Console.WriteLine($"Error while trying to save dog {dog.Id} {dog.Name} in database");
                }
            }
        }

        /// <summary>
        /// Retourne le dernier id disponible dans notre table et on y ajoute + 1 pour obtenir un id libre
        /// </summary>
        /// <returns>Le dernier id + 1 de notre table dogs</returns>
        public int GetNextFreeId()
        {
            if (Connection == null)
                return -1;

            using (var cmd = new NpgsqlCommand("select id from dogs order by id desc limit 1", Connection))
            {
                object? result = cmd.ExecuteScalar();
                if (result == null)
                    return -1;
                
                return Convert.ToInt32(result) + 1;
            }
        }
    }
}