using System.Reflection;
using HttpServer.Routing.Controllers;

namespace HttpServer.Routing;

public class Router
{
    public Dictionary<string,(Type,MethodInfo)> ControllerTypes;

    public Router()
    {
        var userControllerType = typeof(UserController);
        
        ControllerTypes = [];
        
        ControllerTypes.Add("/someRoute/",(userControllerType,userControllerType.GetMethod("SomeRoute")!));
        ControllerTypes.Add("/someRoute/",(userControllerType,userControllerType.GetMethod("SomeRoute")!));
        ControllerTypes.Add("/someRoute/",(userControllerType,userControllerType.GetMethod("SomeRoute")!));

        
    }
}