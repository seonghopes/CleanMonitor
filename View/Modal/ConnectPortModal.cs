using CleanMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMonitor.View.Modal
{
    public partial class ConnectPortModal : Form
    {
        public event EventHandler<ConnectPortEventArgs> PortConnected;
        private readonly string toiletId;
        private readonly string usedPort;

        public ConnectPortModal(string toiletId, string usedPort)
        {
            InitializeComponent();
            this.toiletId = toiletId; 
            this.usedPort = usedPort; 
            LoadComPorts();
        }

        private void LoadComPorts()
        {
            cbPort.Items.Clear();

            string[] ports = SerialPort.GetPortNames();

            if (ports.Length == 0)
            {
                cbPort.Items.Add("포트 없음");
                cbPort.SelectedIndex = 0;
            }
            else
            {
                int selectedIndex = 0; 

                for (int i = 0; i < ports.Length; i++)
                {
                    cbPort.Items.Add(ports[i]);
                    if (usedPort != null && ports[i] == usedPort)
                    {
                        selectedIndex = i;
                    }
                }
                cbPort.SelectedIndex = selectedIndex;

            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cbPort.SelectedItem == null || cbPort.SelectedItem.ToString() == "포트 없음")
            {
                MessageBox.Show("연결가능한 포트가 없습니다.");
                return;
            }

            string selectedPort = cbPort.SelectedItem.ToString();

            PortConnected?.Invoke(this, new ConnectPortEventArgs
            {
                ToiletId = toiletId,
                PortName = selectedPort
            });

            this.Close();
        }

      
    }

    public class ConnectPortEventArgs : EventArgs
    {
        public string ToiletId { get; set; }
        public string PortName { get; set; }
    }
}
