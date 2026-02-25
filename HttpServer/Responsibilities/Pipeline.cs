using HttpServer.General;

namespace HttpServer.Responsibilities;

public class Pipeline
{
    private ThreadLocal<List<IHandler>> _handlers;
    
    public HttpResponse ProcessRequest(HttpRequest request)
    {
        var response = new HttpResponse();
        var session = new Session();
        return response;
    }
}