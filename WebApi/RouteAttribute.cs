namespace csharp_dao.WebApi
{
    /// <summary>
    /// Attribut route permettant de définir une route (URL/chemin) à gérer sur notre API
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RouteAttribute : Attribute
    {
        public string Path { get; }

        public RouteAttribute(string path)
        {
            Path = path.ToLower();
        }
    }
}