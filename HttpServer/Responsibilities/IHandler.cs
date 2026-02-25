using HttpServer.General;

namespace HttpServer.Responsibilities;

public interface IHandler
{
    IHandler Next { get; set; }
    
    HttpResponse Execute(HttpRequest request);
}