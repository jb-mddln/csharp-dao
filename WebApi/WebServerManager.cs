using Npgsql;
using System.Net;
using System.Reflection;
using System.Text;

namespace csharp_dao.WebApi
{
    public class WebServerManager
    {
        public HttpListener Listener { get; set; }
        
        public WebServerManager(string address = "localhost", int port = 8080)
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add($"http://{address}:{port}/");
        }

        /// <summary>
        /// Démarre notre serveur web
        /// </summary>
        public void Start()
        {
            Listener.Start();
        }

        /// <summary>
        /// Gère les routes de notre api
        /// </summary>
        /// <param name="databaseConnection">Connexion à notre base de données</param>
        public void HandleRequestRoute(NpgsqlConnection? databaseConnection)
        {
            if (!Listener.IsListening)
                return;

            if (databaseConnection == null)
                return;

            HttpListenerContext context = Listener.GetContext();

            HttpListenerRequest request = context.Request;

            HttpListenerResponse response = context.Response;

            if (request.Url == null)
                return;

            string path = request.Url.AbsolutePath.ToLower();

            foreach (MethodInfo method in typeof(RouteController).GetMethods())
            {
                // exemple method = HandleDogGet

                // trouve tout nos [Route("")] au-dessus de nos méthodes
                object[] attributes = method.GetCustomAttributes(typeof(RouteAttribute), true);

                // Aucune route trouvée
                if (attributes.Length == 0)
                {
                    // SendResponse(response, HttpStatusCode.InternalServerError, "Error: No route available in API");
                    return;
                }

                // ne dois logiquement jamais arrivée ?
                // if (attributes[0] is not RouteAttribute routeAttribute)
                if (attributes.All(attribute => attribute is not RouteAttribute))
                {
                    // SendResponse(response, HttpStatusCode.InternalServerError, $"Error: unknown {attributes[0]} is not a routeAttribute");
                    return;
                }

                RouteAttribute routeAttribute = (RouteAttribute)attributes.FirstOrDefault(attribute => (attribute as RouteAttribute).Path == path);

                // notre route n'est pas égale à notre url entrée
                //if (routeAttribute.Path.ToLower() != path)

                // aucune route trouvée qui ne pourrait correspondre à nos attributs
                if (routeAttribute == null)
                {
                    // SendErrorStatus(response, HttpStatusCode.NotFound, $"No route found for {path}");
                    SendResponse(response, HttpStatusCode.NotFound, $"Error: no route found for {path}");
                    return;
                }

                // créer une nouvelle instance de notre class RouteController, pour ensuite pouvoir appeler les méthodes contenues dans notre class
                object? routeControllerInstance = Activator.CreateInstance(typeof(RouteController));
                // ne dois logiquement jamais arrivée ?
                if (routeControllerInstance == null)
                {
                    SendResponse(response, HttpStatusCode.InternalServerError, "Error: unknown can't create route controller instance");
                }

                // tout est bon on invoque (appelle) notre method HandleDogGet par exemple avec en paramètre le context
                method.Invoke(routeControllerInstance, new object[] { context });
            }
        }

        /// <summary>
        /// Envoie notre réponse au client
        /// </summary>
        /// <param name="response"></param>
        /// <param name="status"></param>
        /// <param name="content"></param>
        public static void SendResponse(HttpListenerResponse response, HttpStatusCode status, string content)
        {
            response.StatusCode = (int)status;
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.Close();
        }
    }
}
