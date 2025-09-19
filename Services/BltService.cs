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

        public BltService(string comPort = "COM3", int baudRate = 9600)
        {
            serialPort = new SerialPort(comPort, baudRate);
            serialPort.DataReceived += SerialPort_DataReceived;

            try { serialPort.Open(); }
            catch (Exception ex) { Console.WriteLine("블루투스 연결 실패: " + ex.Message); }
        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                
            }
            catch { }

            //string data = serialPort.ReadLine();
            //    this.Invoke(new Action(() =>
            //    {
            //        lblDist.Text = data; // UI에 표시

            //        if (float.TryParse(data, out float distance))
            //        {

            //        }
            //    }));
        }

        public void Close() { if (serialPort.IsOpen) serialPort.Close(); }
    }
}
