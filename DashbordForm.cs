using CleanMonitor.Models;
using CleanMonitor.Repository;
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

        private List<ToiletStatus> toiletStatusList = new List<ToiletStatus>();

        private ToiletRepository repository = new ToiletRepository();

        private Dictionary<string, MainCard> IdMapCard = new Dictionary<string, MainCard>();

        public DashbordForm()
        {
            InitializeComponent();

            FormSizeInit();
            AddDummyCard();
            AddStuatusCard();

            LoadToiletStatusList();

        }
        private void FormSizeInit()
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1024, 860);
            this.MaximumSize = new Size(1920, 1080);
        }

        public void AddStuatusCard()
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
            statusCard2.SetText("교체 필요");
            statusCard3.SetText("주의 필요");
            statusCard4.SetText("정상 상태");
        }

        private void AddDummyCard()
        {
            for (int row = 0; row < tlpMainCard.RowCount; row++)
                for (int col = 0; col < tlpMainCard.ColumnCount; col++)
                {
                    DummyCard dummyCard = new DummyCard();

                    dummyCard.OpenAddModal += ShowAddModal; // DummyCard Class 이벤트 연결

                    tlpMainCard.Controls.Add(dummyCard.dummyPanel, col, row);
                }
        }

        private void LoadToiletStatusList()
        {
            toiletStatusList = repository.LoadAll();
            foreach (ToiletStatus status in toiletStatusList)
            {
                AddMainCard(status);
            }
        }

 
        private void AddMainCard(ToiletStatus status)
        {
            MainCard mc = new MainCard();
            mc.SetSection(status);
            mc.OpenDeleteModal += ShowDeleteModal;

            for (int row = 0; row < tlpMainCard.RowCount; row++)
                for (int col = 0; col < tlpMainCard.ColumnCount; col++)
                {
                    Control ctrl = tlpMainCard.GetControlFromPosition(col, row);
                    if (ctrl != null && ctrl.Tag?.ToString() == "Dummy")
                    {
                        tlpMainCard.Controls.Remove(ctrl);
                        tlpMainCard.Controls.Add(mc.mainPanel, col, row);

                        if (!IdMapCard.ContainsKey(status.ToiletId))
                            IdMapCard.Add(status.ToiletId, mc);

                        return;
                    }
                }
        }


        private void ShowAddModal(object sender, EventArgs e)
        {
            MainCardAddModal addModal = new MainCardAddModal();

            addModal.MainCardAdd += (s, status) =>
            {
                AddMainCard(status);
                toiletStatusList.Add(status);
                repository.SaveAll(toiletStatusList);
            };

            addModal.ShowDialog();
        }



        private void ShowDeleteModal(object sender, string toiletId)
        {
            if (!IdMapCard.ContainsKey(toiletId)) return;

            MainCard mc = IdMapCard[toiletId];

            MainCardDeleteModal deleteModal = new MainCardDeleteModal();

            deleteModal.MainCardDelete += (s, args) =>
            {
                tlpMainCard.Controls.Remove(mc.mainPanel);

                IdMapCard.Remove(toiletId);

                ToiletStatus ts = toiletStatusList.FirstOrDefault(x => x.ToiletId == toiletId);
                if (ts != null)
                {
                    toiletStatusList.Remove(ts);
                    repository.SaveAll(toiletStatusList);
                }

                TableLayoutPanelCellPosition pos = tlpMainCard.GetPositionFromControl(mc.mainPanel);
                DummyCard dummyCard = new DummyCard();
                dummyCard.OpenAddModal += ShowAddModal;
                tlpMainCard.Controls.Add(dummyCard.dummyPanel, pos.Column, pos.Row);
            };

            deleteModal.ShowDialog();
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
