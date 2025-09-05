using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMonitor
{
    public partial class DashbordForm : Form
    {
        public DashbordForm()
        {
            InitializeComponent();
            CustomStyleInit();
        }

        private void CustomStyleInit()
        {
            //tlpStatusCard.MaximumSize = new Size(0, 180);
            //tlpStatusCard.Padding = new Padding(15, 10, 15, 10);
            //tlpMain.Padding = new Padding(15, 10, 15, 10);
            //tlpMain.Dock = DockStyle.Fill;

            //Panel[] statusPanels = { statusCard1, statusCard1, statusCard1, statusCard1 };
            //foreach (var panel in statusPanels)
            //{
            //    panel.Margin = new Padding(10);
            //}

            //Panel[] mainPanels = { pMain1, pMain2, pMain3, pMain4, pMain5 };
            //foreach (var panel in mainPanels)
            //{
            //    panel.Margin = new Padding(10);
            //}

            StatusCard statusCard1 = new StatusCard();
            StatusCard statusCard2 = new StatusCard();
            StatusCard statusCard3 = new StatusCard();
            StatusCard statusCard4 = new StatusCard();
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
            tlpMainCard.Controls.Add(mainCard1.mainPanel, 0, 0);
            tlpMainCard.Controls.Add(mainCard2.mainPanel, 1, 0);
            tlpMainCard.Controls.Add(mainCard3.mainPanel, 2, 0);
            tlpMainCard.Controls.Add(mainCard4.mainPanel, 3, 0);
            tlpMainCard.Controls.Add(mainCard5.mainPanel, 4, 0);

        }
    }
}
