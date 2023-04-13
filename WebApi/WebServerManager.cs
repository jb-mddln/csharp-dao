using System.Net;
using System.Reflection;

namespace csharp_dao.WebApi
{
    public class WebServerManager
    {
        public HttpListener Listener { get; set; }

        public WebServerManager(string address = "localhost", int port = 8080)
        {
            Listener = new HttpListener();
            Listener.Prefixes.Add($"http://{address}:{port}/");
            // Listener.Start();
        }

        public void Start()
        {
            Listener.Start();
        }

        public void HandleRequestRoute()
        {
            if (!Listener.IsListening)
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
                    return;

                // ne dois logiquement jamais arrivée ?
                if (attributes[0] is not RouteAttribute routeAttribute)
                    return;

                // notre route n'est pas égale à notre url entrée
                if (routeAttribute.Path.ToLower() != path)
                    return;

                // créer une nouvelle instance de notre class RouteController, pour ensuite pouvoir appeler les méthodes contenues dans notre class
                object? routeControllerInstance = Activator.CreateInstance(typeof(RouteController));
                // ne dois logiquement jamais arrivée ?
                if (routeControllerInstance == null)
                    return;

                // tout est bon on invoque (appelle) notre method HandleDogGet par exemple avec en paramètre le context
                method.Invoke(routeControllerInstance, new object[] { context });
            }
        }
    }
}
