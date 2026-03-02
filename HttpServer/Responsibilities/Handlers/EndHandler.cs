using HttpServer.General;

namespace HttpServer.Responsibilities;

public class EndHandler : IHandler
{
    public IHandler Next { get; set; }
    public HttpResponse Execute(HttpRequest request)
    {
        throw new NotImplementedException();
    }
}