using CleanMonitor.Models;
using CleanMonitor.View.Modal;
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
        public DashbordForm()
        {
            InitializeComponent();
            CustomStyleInit();

        }
        private void CustomStyleInit()
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1024, 860);
            this.MaximumSize = new Size(1920, 1080);

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
            statusCard2.SetText("교체 필요");
            statusCard3.SetText("주의 필요");
            statusCard4.SetText("정상 상태");

            for (int row = 0; row < tlpMainCard.RowCount; row++)
                for (int col = 0; col < tlpMainCard.ColumnCount; col++)
                {
                    DummyCard dummyCard = new DummyCard();

                    dummyCard.dummyPanel.Tag = "Dummy";
                    dummyCard.AddClick += Dummy_AddButtonClick;

                    tlpMainCard.Controls.Add(dummyCard.dummyPanel, col, row);
                  }

          
        }
        private void Dummy_AddButtonClick(object sender, EventArgs e)
        {
            MainCardAdd addModdal = new MainCardAdd();
            addModdal.MainCardAdded += AddModal_MainCardAdd;
            addModdal.ShowDialog();
        }

        private void AddModal_MainCardAdd(object sender, ToiletStatus status)
        {
            MainCard mc = new MainCard();
            mc.SetMainSection(status.MainSection);
            mc.SetSubSection(status.SubSection);
            for (int row = 0; row < tlpMainCard.RowCount; row++)
                for (int col = 0; col < tlpMainCard.ColumnCount; col++)
                {
                    Control ctrl = tlpMainCard.GetControlFromPosition(col, row);
                    if (ctrl != null && ctrl.Tag?.ToString() == "Dummy")
                    {
                        tlpMainCard.Controls.Remove(ctrl);
                        tlpMainCard.Controls.Add(mc.mainPanel,col,row);
                        return;
                    }
                }
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
