using System.Net;

namespace HttpServer.General;

public class HttpRequest(HttpListenerRequest request)
{
    public string Url => request.RawUrl 
                         ?? throw new InvalidOperationException("URL nicht vorhanden in Request.");
}