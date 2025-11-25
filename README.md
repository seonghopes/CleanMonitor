// TcpReceiveService.cs
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class TcpReceiveService
{
    public event EventHandler<string> DataReceived;

    public TcpReceiveService()
    {
        Task.Run(() => StartServer());
    }

    private void StartServer()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();
        
        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            Task.Run(() => HandleClient(client));
        }
    }

    private void HandleClient(TcpClient client)
    {
        var stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int len;

        while ((len = stream.Read(buffer, 0, buffer.Length)) > 0)
        {
            string message = Encoding.UTF8.GetString(buffer, 0, len);
            DataReceived?.Invoke(this, message);
        }
    }
}
