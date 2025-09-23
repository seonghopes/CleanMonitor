using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanMonitor.Services
{
    internal class BltService
    {
        private SerialPort serialPort;

        public event EventHandler<string> serialEvent;
        public string ToiletId { get; private set; }

        public BltService(string toiletId, string port)
        {
            this.ToiletId = toiletId;
            serialPort = new SerialPort(port, 9600);
            serialPort.DataReceived += SerialPort_DataReceived;

            try { serialPort.Open(); }
            catch (Exception ex) { Console.WriteLine("블루투스 연결 실패: " + ex.Message); }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPort.ReadLine();
                serialEvent?.Invoke(sender, data);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Close() { if (serialPort.IsOpen) serialPort.Close(); }
    }
}
