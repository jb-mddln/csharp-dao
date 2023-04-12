﻿using csharp_dao.Database.Repository;
using csharp_dao.Database.Tables;
using Npgsql;

namespace csharp_dao
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            NpgsqlDataSourceBuilder dataSourceBuilder = new NpgsqlDataSourceBuilder("host=localhost;username=postgres;password=root;database=Simplon;");
            NpgsqlDataSource dataSource = dataSourceBuilder.Build();
            NpgsqlConnection connection = dataSource.OpenConnection();
            
            DogRepository dogRepository = new DogRepository(connection);

            List<Dog> allDogs = dogRepository.FindAll();
            List<Dog> twoDogs = dogRepository.FindAll(2);

            Console.WriteLine(string.Join("\n", allDogs.Select(dog => dog.ToString())));
            Console.WriteLine(string.Join("\n", twoDogs.Select(dog => dog.ToString())));

            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}