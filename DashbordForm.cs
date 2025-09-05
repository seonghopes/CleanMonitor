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
            tlpStatusCard.MaximumSize = new Size(0, 180);
            tlpStatusCard.Padding = new Padding(15, 10, 15, 10);
            tlpMain.Padding = new Padding(15, 10, 15, 10);
            tlpMain.Dock = DockStyle.Fill;

            //Panel[] statusPanels = { pStatus1, pStatus2, pStatus3, pStatus4 };
            //foreach (var panel in statusPanels)
            //{
            //    panel.Margin = new Padding(10);
            //}

            //Panel[] mainPanels = { pMain1, pMain2, pMain3, pMain4, pMain5 };
            //foreach (var panel in mainPanels)
            //{
            //    panel.Margin = new Padding(10);
            //}
        }
    }
}
