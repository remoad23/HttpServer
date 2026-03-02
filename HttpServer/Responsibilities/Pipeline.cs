using HttpServer.General;

namespace HttpServer.Responsibilities;

public class Pipeline
{
    private ThreadLocal<List<IHandler>> _handlers;

    public Pipeline()
    {
        _handlers = new ThreadLocal<List<IHandler>>();
        _handlers.Value.Add(new StartHandler
        {
            Next = _handlers.Value[1]
        });
        _handlers.Value.Add(new EndHandler());
    }
    
    public HttpResponse ProcessRequest(HttpRequest request)
    {
        var response = new HttpResponse();
        var session = new Session();
        _handlers.Value[0].Execute(request);
        
        return response;
    }
}