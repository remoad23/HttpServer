using HttpServer.General;

namespace HttpServer.Responsibilities;

public class StartHandler : IHandler
{
    public IHandler Next { get; set; }
    public HttpResponse Execute(HttpRequest request)
    {
        throw new NotImplementedException();
    }
}