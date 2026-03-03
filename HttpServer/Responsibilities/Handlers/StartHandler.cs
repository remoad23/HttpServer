using HttpServer.General;

namespace HttpServer.Responsibilities;

public class StartHandler : IHandler
{
    public IHandler Next { get; set; }
    public IHandler Execute(HttpRequest request)
    {
        return Next.Execute(request);
    }
}