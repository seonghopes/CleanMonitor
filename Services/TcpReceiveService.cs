using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanMonitor.Services
{
    internal class TcpReceiveService
    {
        public event EventHandler<string> dataReceived;

        public string ToiletId { get; private set; }

        private TcpListener server;
        private CancellationTokenSource cts;

        public TcpReceiveService(string toiletId)
        {
            ToiletId = toiletId;

            cts = new CancellationTokenSource();
            Task.Run(() => StartServer(cts.Token));
        }

        private async Task StartServer(CancellationToken token)
        {
            try
            {
                server = new TcpListener(IPAddress.Any, 5000);
                server.Start();

                while (!token.IsCancellationRequested)
                {
                    var client = await server.AcceptTcpClientAsync();

                    _ = Task.Run(() => HandleClient(client, token));
                }
            }
            catch (ObjectDisposedException)
            {
                // 서버가 정상적으로 종료될 때 발생 가능 → 무시 가능
            }
            catch (Exception ex)
            {
                Console.WriteLine("TCP Server Error: " + ex.Message);
            }
        }

        private async Task HandleClient(TcpClient client, CancellationToken token)
        {
            using (client)
            using (var stream = client.GetStream())
            {
                byte[] buffer = new byte[1024];

                try
                {
                    while (!token.IsCancellationRequested)
                    {
                        int len = await stream.ReadAsync(buffer, 0, buffer.Length, token);
                        if (len <= 0) break;

                        string message = Encoding.UTF8.GetString(buffer, 0, len);
                        dataReceived?.Invoke(this, message);
                    }
                }
                catch (OperationCanceledException)
                {
                    // 취소된 경우 → 조용히 종료
                }
                catch (Exception ex)
                {
                    Console.WriteLine("TCP Client Error: " + ex.Message);
                }
            }
        }

        public void Close()
        {
            try
            {
                cts?.Cancel();
                server?.Stop();
            }
            catch (Exception ex)
            {
                Console.WriteLine("TCP Close Error: " + ex.Message);
            }
        }
    }
}