using HttpServer.General;

namespace HttpServer.Responsibilities;

public interface IHandler
{
    IHandler Next { get; set; }
    
    IHandler Execute(HttpRequest request);
}