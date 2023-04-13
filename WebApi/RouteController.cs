using System.Net;
using System.Text;

namespace csharp_dao.WebApi
{
    public class RouteController
    {
        public RouteController()
        {

        }

        /// <summary>
        /// Gère notre route (URL/chemin) / & /api/dog/get
        /// </summary>
        /// <param name="context"></param>
        [Route("/"), Route("/api/dog/get")]
        public void HandleDogGet(HttpListenerContext context)
        {
            /*var dog = new Dog("Fido", "Corgi", new DateTime(2022, 10, 21), 1);
            var json = JsonSerializer.Serialize(dog);*/

            WebServerManager.SendResponse(context.Response, HttpStatusCode.OK, "OK");
        }
    }
}
