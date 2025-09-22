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
        public ConnectPortModal()
        {
            InitializeComponent();
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
                cbPort.Items.AddRange(ports);
                cbPort.SelectedIndex = 0;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (cbPort.SelectedItem != null)
            {
                string selectedPort = cbPort.SelectedItem.ToString();
                MessageBox.Show($"선택된 포트: {selectedPort}");
            }
        }

        private void cbPort_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
    }
}
