using csharp_dao.Database;
using csharp_dao.Database.Repository;
using csharp_dao.Database.Tables;
using Npgsql;
using System.Net;
using System.Text;
using System.Text.Json;
using csharp_dao.WebApi;
using static System.Net.Mime.MediaTypeNames;

namespace csharp_dao
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            DatabaseManager databaseManager = new DatabaseManager();
            databaseManager.OpenConnection();

            List<Dog> allDogs = new List<Dog>();

            if (databaseManager.Connection != null)
            {
                DogRepository dogRepository = new DogRepository(databaseManager.Connection);
                allDogs = dogRepository.FindAll();

                /* List<Dog> twoDogs = dogRepository.FindAll(2);

                Console.WriteLine(string.Join("\n", allDogs.Select(dog => dog.ToString())));
                Console.WriteLine(string.Join("\n", twoDogs.Select(dog => dog.ToString()))); */

                /* Dog newDog = new Dog();
                newDog.Id = dogRepository.GetNextFreeId();
                newDog.Name = "Boby";
                newDog.Breed = "Carlin";
                newDog.Birthdate = DateTime.Now;
    
                dogRepository.Save(newDog); 

                Dog? finDog = dogRepository.FindById(2);
                if (finDog == null)
                {
                    Console.WriteLine("No dog found with the id 2");
                }
                else
                {
                    Console.WriteLine(finDog.ToString());
                } */
            }

            WebServerManager webServerManager = new WebServerManager();
            webServerManager.Start();

            while (true)
            {
                webServerManager.HandleRequestRoute();
            }
        }
    }
}