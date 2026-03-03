using HttpServer.General;
using HttpServer.Routing;

namespace HttpServer.Responsibilities;

public class RoutingHandler : IHandler
{
    public IHandler Next { get; set; }
    public IHandler Execute(HttpRequest request)
    {
        var router = new Router();
        var (controller, method) = router.ControllerTypes[request.Url];
        
        var instance = Activator.CreateInstance(controller);
        method.Invoke(instance,[]);

        // TODO: return parameter richtig einstellen für die pipeline
        // danach placeholder routes + daten einfügen
        // danach multithreading
        return Next.Execute(request);
    }
}