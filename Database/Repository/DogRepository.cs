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

        }
    }
}
