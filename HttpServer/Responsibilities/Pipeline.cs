using HttpServer.General;

namespace HttpServer.Responsibilities;

public class Pipeline
{
    private ThreadLocal<List<IHandler>> _handlers;

    public Pipeline()
    {
        _handlers = new ThreadLocal<List<IHandler>>();
        _handlers.Value.Add(new StartHandler());
        _handlers.Value.Add(new RoutingHandler());
        _handlers.Value.Add(new EndHandler());

        for (var x = 0; x < _handlers.Value.Count-1; x++)
        {
            _handlers.Value[x].Next = _handlers.Value[x+1];
        }
    }
    
    public HttpResponse ProcessRequest(HttpRequest request)
    {
        var response = new HttpResponse();
        var session = new Session();
        _handlers.Value[0].Execute(request);
        
        return response;
    }
}