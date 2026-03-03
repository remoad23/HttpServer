using System.Reflection;

namespace HttpServer.Routing;

public class BaseController
{
    public void GetAllControllerTypes()
    {
        var derivedTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseController)))
            .ToList();
    }
}