using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMonitor
{
    public partial class DashbordForm : Form
    {
        SerialPort serialDtsensor;
        public DashbordForm()
        {
            InitializeComponent();
            CustomStyleInit();

            this.WindowState = FormWindowState.Maximized; 
            this.MinimumSize = new Size(920, 860);
            this.MaximumSize = new Size(1920, 1080);


            //serialDtsensor = new SerialPort("COM3", 9600);
            //serialDtsensor.DataReceived += Serial_DataReceived;
            //serialDtsensor.Open();
        }

        //private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    string data = serialDtsensor.ReadLine();
        //    this.Invoke(new Action(() =>
        //    {
        //        lblDist.Text = data; // UI에 표시

        //        if (float.TryParse(data, out float distance))
        //        {

        //        }
        //    }));
        //}

        
        private void CustomStyleInit()
        {
            StatusCard statusCard1 = new StatusCard(Color.LightSteelBlue, LoadImageSafe(@"C:\Images\toilet.png"));
            StatusCard statusCard2 = new StatusCard(Color.LightPink, LoadImageSafe(@"C:\Images\siren.png"));
            StatusCard statusCard3 = new StatusCard(Color.LightGoldenrodYellow, LoadImageSafe(@"C:\Images\warn.png"));
            StatusCard statusCard4 = new StatusCard(Color.LightGreen, LoadImageSafe(@"C:\Images\check.png"));
            tlpStatusCard.Controls.Add(statusCard1.statusPanel, 0, 0);
            tlpStatusCard.Controls.Add(statusCard2.statusPanel, 1, 0);
            tlpStatusCard.Controls.Add(statusCard3.statusPanel, 2, 0);
            tlpStatusCard.Controls.Add(statusCard4.statusPanel, 3, 0);
            statusCard1.SetCnt(5);
            statusCard2.SetCnt(7);
            statusCard3.SetCnt(7);
            statusCard4.SetCnt(14);
            statusCard1.SetText("총 화장실");
            statusCard2.SetText("긴급 상황");
            statusCard3.SetText("주의 필요");
            statusCard4.SetText("정상 상태");

            MainCard mainCard1 = new MainCard();
            MainCard mainCard2 = new MainCard();
            MainCard mainCard3 = new MainCard();
            MainCard mainCard4 = new MainCard();
            MainCard mainCard5 = new MainCard();
            MainCard mainCard6 = new MainCard();
            MainCard mainCard7 = new MainCard();
            MainCard mainCard8 = new MainCard();
            MainCard mainCard9 = new MainCard();
            MainCard mainCard10 = new MainCard();
            //DummyCard dummyCard1 = new DummyCard();
            //DummyCard dummyCard2 = new DummyCard();
            //DummyCard dummyCard3 = new DummyCard();
            //DummyCard dummyCard4 = new DummyCard();
            //DummyCard dummyCard5 = new DummyCard();
            //DummyCard dummyCard6 = new DummyCard();
            //DummyCard dummyCard7 = new DummyCard();
            DummyCard dummyCard8 = new DummyCard();
            //tlpMainCard.Controls.Add(dummyCard1.dummyPanel, 2, 0);
            //tlpMainCard.Controls.Add(dummyCard2.dummyPanel, 3, 0);
            //tlpMainCard.Controls.Add(dummyCard3.dummyPanel, 4, 0);
            //tlpMainCard.Controls.Add(dummyCard4.dummyPanel, 5, 0);
            //tlpMainCard.Controls.Add(dummyCard5.dummyPanel, 6, 0);
            //tlpMainCard.Controls.Add(dummyCard6.dummyPanel, 7, 0);
            //tlpMainCard.Controls.Add(dummyCard7.dummyPanel, 8, 0);
            tlpMainCard.Controls.Add(dummyCard8.dummyPanel, 9, 0);
            tlpMainCard.Controls.Add(mainCard1.mainPanel, 0, 0);
            tlpMainCard.Controls.Add(mainCard2.mainPanel, 1, 0);
            tlpMainCard.Controls.Add(mainCard3.mainPanel, 2, 0);
            tlpMainCard.Controls.Add(mainCard4.mainPanel, 3, 0);
            tlpMainCard.Controls.Add(mainCard5.mainPanel, 4, 0);
            tlpMainCard.Controls.Add(mainCard6.mainPanel, 5, 0);
            tlpMainCard.Controls.Add(mainCard7.mainPanel, 6, 0);
            tlpMainCard.Controls.Add(mainCard8.mainPanel, 7, 0);
            tlpMainCard.Controls.Add(mainCard9.mainPanel, 8, 0);
            //tlpMainCard.Controls.Add(mainCard10.mainPanel, 9, 0);
        }

        private Image LoadImageSafe(string path)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    return Image.FromFile(path);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"이미지 로드 실패: {ex.Message}");
                return null;
            }
        }
    }
}
