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

    public static string GetView(string viewName)
    {
        var path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.FullName;
        
        using var htmlStream = new FileStream(Path.Combine(path, viewName + ".html"), FileMode.Open);
        using var cssStream = new FileStream(Path.Combine(path, viewName + ".css"), FileMode.Open);
        
        using var htmlStreamReader = new StreamReader("");
        using var cssStreamReader = new StreamReader("");

        // hier css in html einbinden 
        while (htmlStreamReader.ReadLine() is { })
        {
            
        }
        
        
    }
}