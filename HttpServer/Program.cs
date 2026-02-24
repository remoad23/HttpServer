using System.Net;
using System.Net.Sockets;
using System.Text;


// SCRAP IT UND BENUTZT HPTTPLISTENER
// This example requires the System and System.Net namespaces.
static void SimpleListenerExample(string[] prefixes)
{
    if (!HttpListener.IsSupported)
    {
        Console.WriteLine ("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
        return;
    }
    // URI prefixes are required,
    // for example "http://contoso.com:8080/index/".
    // if (prefixes == null || prefixes.Length == 0)
    //     throw new ArgumentException("prefixes");

    // Create a listener.
    HttpListener listener = new HttpListener();
    listener.Prefixes.Add("http://localhost:8080/");
    // Add the prefixes.
    foreach (string s in prefixes)
    {
        listener.Prefixes.Add(s);
    }
    listener.Start();
    Console.WriteLine("Listening...");
    // Note: The GetContext method blocks while waiting for a request.
    HttpListenerContext context = listener.GetContext();
    HttpListenerRequest request = context.Request;
    // Obtain a response object.
    HttpListenerResponse response = context.Response;
    // Construct a response.
    string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
    // Get a response stream and write the response to it.
    response.ContentLength64 = buffer.Length;
    System.IO.Stream output = response.OutputStream;
    output.Write(buffer,0,buffer.Length);
    // You must close the output stream.
    output.Close();
    listener.Stop();
}

SimpleListenerExample([]);








































// var hostName = Dns.GetHostName();
// IPHostEntry localhost = await Dns.GetHostEntryAsync(hostName);
// // This is the IP address of the local machine
// IPAddress localIpAddress = localhost.AddressList[0];
// var ipEndPoint = new IPEndPoint(localIpAddress, 13);
// TcpListener listener = new(ipEndPoint);
//
// try
// {    
//     listener.Start();
//
//     using TcpClient handler = await listener.AcceptTcpClientAsync();
//     await using NetworkStream stream = handler.GetStream();
//
//     int i;
//     // Buffer for reading data
//     Byte[] bytes = new Byte[256];
//     String data = null;
//     // ncat fe80::cf7a:cc6d:87de:739f%4 13 in cmd ausführen
//     while((i = stream.Read(bytes, 0, bytes.Length))!=0)
//     {
//         // Translate data bytes to a ASCII string.
//         data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
//         Console.WriteLine("Received: {0}", data);
//
//         // Process the data sent by the client.
//         data = data.ToUpper();
//
//         byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);
//
//         // Send back a response.
//         stream.Write(msg, 0, msg.Length);
//         Console.WriteLine("Sent: {0}", data);
//     }
//
// }
// finally
// {
//     listener.Stop();
// }