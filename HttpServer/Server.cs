using System.Net;
using HttpServer.General;
using HttpServer.Responsibilities;

namespace HttpServer;

public class Server
{
    private HttpListener _listener;
    private Lock _sendLocker;

    private Database.Database _database;
    
    public Server(List<string> prefixes)
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add("http://localhost:8080/");
        
        foreach (string s in prefixes)
        {
            _listener.Prefixes.Add(s);
        }

        _database = new Database.Database();
    }
    
    public void Start()
    {
        Console.WriteLine("Server starting...");
        _listener.Start();
        Console.WriteLine("Listening...");
        ListenToRequests();
    }

    public void ListenToRequests()
    {
        while (_listener.IsListening)
        {
            Task.Run(async () =>
            {
                var request = await GetRequest();
                var response = ProcessRequest(request);
                SendResponse(request);
            });
        }
    }

    private HttpResponse ProcessRequest(HttpRequest request)
    {
        var pipeline = new Pipeline();
        var response = pipeline.ProcessRequest(request);
        return response;
    }

    private async Task<HttpRequest> GetRequest()
    {
        // Note: The GetContext method blocks while waiting for a request.
        var request = await _listener.GetContextAsync();
        var httpRequest = new HttpRequest(); 
        return httpRequest;
    }

    private void SendResponse(HttpRequest request)
    {
        lock(_sendLocker)
        {
            var response = _listener.GetContext().Response;

            // hier die verarbeitung (neue thread?)
            var responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            var buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            
            // hier irgendwie triggern, dass ein weiterer thread zugelassen wird weiter zu gehen
            var output = response.OutputStream;
            output.Write(buffer,0,buffer.Length);
            output.Close();
        }
    }

    private void Quit()
    {
        _listener.Stop();
    }
}