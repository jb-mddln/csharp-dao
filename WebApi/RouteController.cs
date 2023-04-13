using csharp_dao.Database.Tables;
using System.Net;
using System.Text.Json;
using System.Text;

namespace csharp_dao.WebApi
{
    public class RouteController
    {
        [Route("/api/dog/get")]
        public void HandleDogGet(HttpListenerContext context)
        {
            /*var dog = new Dog("Fido", "Corgi", new DateTime(2022, 10, 21), 1);
            var json = JsonSerializer.Serialize(dog);*/

            byte[] buffer = Encoding.UTF8.GetBytes("ok");

            context.Response.Headers.Add("Content-Type", "application/json");
            context.Response.ContentLength64 = buffer.Length;
            context.Response.OutputStream.Write(buffer, 0, buffer.Length);

        }
    }
}
