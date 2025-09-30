using CleanMonitor.Models;
using CleanMonitor.Repository;
using CleanMonitor.Services;
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
        private ToiletRepository repository = new ToiletRepository();
        private List<ToiletStatus> toiletStatusList = new List<ToiletStatus>();
        private Dictionary<string, MainCard> IdMapCard = new Dictionary<string, MainCard>();

        private PortRepository portRepository = new PortRepository();
        private List<ToiletPort> toiletPorts = new List<ToiletPort>();

        private List<BltService> bltServices = new List<BltService>();

        public DashbordForm()
        {
            InitializeComponent();

            FormSizeInit();
            AddDummyCard();
            AddStuatusCard();

            LoadMainCardList();
        }
        
        private void FormSizeInit()
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1280, 960);
            this.MaximumSize = new Size(1920, 1080);
        }

        public void AddStuatusCard()
        {

            StatusCard all = new StatusCard("총 화장실", Color.LightSteelBlue, LoadImageSafe(@"C:\Images\toilet.png"));
            StatusCard siren = new StatusCard("교체 필요", Color.LightPink, LoadImageSafe(@"C:\Images\siren.png"));
            StatusCard wran = new StatusCard("주의 상태", Color.LightGoldenrodYellow, LoadImageSafe(@"C:\Images\warn.png"));
            StatusCard check = new StatusCard("정상 상태",Color.LightGreen, LoadImageSafe(@"C:\Images\check.png"));
            tlpStatusCard.Controls.Add(all.statusPanel, 0, 0);
            tlpStatusCard.Controls.Add(siren.statusPanel, 1, 0);
            tlpStatusCard.Controls.Add(wran.statusPanel, 2, 0);
            tlpStatusCard.Controls.Add(check.statusPanel, 3, 0);
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
        private void LoadMainCardList()
        {
            toiletStatusList = repository.LoadAll();
            foreach (ToiletStatus status in toiletStatusList)
            {
                AddMainCard(status);
            }

            toiletPorts = portRepository.LoadAll();
            foreach (ToiletPort tp in toiletPorts)
            {
                if (IdMapCard.ContainsKey(tp.ToiletId))
                {
                    BltService bltService = new BltService(tp.ToiletId, tp.PortName);
                    bltService.serialEvent += (s, data) =>
                    {
                        this.Invoke(new Action(() =>
                        {
                            if (IdMapCard.ContainsKey(tp.ToiletId))
                            {
                                IdMapCard[tp.ToiletId].SetData(data);
                            }
                        }));
                    };
                    bltServices.Add(bltService);
                }
            }
        }
        //private void LoadMainCardList()
        //{
        //    Task.Run(() =>
        //    {
        //        List<ToiletStatus> tss = repository.LoadAll();
        //        List<ToiletPort> tps = portRepository.LoadAll();

        //        this.BeginInvoke(new Action(() =>
        //            {
        //                toiletStatusList = tss;
        //                toiletPorts = tps;

        //                foreach (ToiletStatus status in toiletStatusList)
        //                {
        //                    AddMainCard(status);
        //                }

        //                foreach (ToiletPort tp in toiletPorts)
        //                {
        //                    if (IdMapCard.ContainsKey(tp.ToiletId))
        //                    {
        //                        BltService bltService = new BltService(tp.ToiletId, tp.PortName);
        //                        bltService.serialEvent += (s, data) =>
        //                        {
        //                            // 이벤트 호출 스레드가 UI 스레드가 아님
        //                            // BeginInvoke (비동기),  Invoke (동기)
        //                            this.BeginInvoke(new Action(() =>
        //                            {
        //                                if (IdMapCard.ContainsKey(tp.ToiletId))
        //                                {
        //                                    IdMapCard[tp.ToiletId].SetData(data);
        //                                }
        //                            }));
        //                        };
        //                        bltServices.Add(bltService);
        //                    }
        //                }

        //            }));

        //    });

        //}


        private void AddMainCard(ToiletStatus status)
        {
            MainCard mc = new MainCard();
            mc.SetSection(status);
            mc.OpenDeleteModal += ShowDeleteModal;
            mc.OpenConnectModal += ShowConnectModal;

            for (int row = 0; row < tlpMainCard.RowCount; row++)
                for (int col = 0; col < tlpMainCard.ColumnCount; col++)
                {
                    Control ctrl = tlpMainCard.GetControlFromPosition(col, row);
                    if (ctrl != null && ctrl.Tag?.ToString() == "Dummy")
                    {
                        tlpMainCard.Controls.Remove(ctrl);
                        ctrl.Dispose();
                        tlpMainCard.Controls.Add(mc.mainPanel, col, row);

                        if (!IdMapCard.ContainsKey(status.ToiletId))
                            IdMapCard.Add(status.ToiletId, mc);

                        return;
                    }
                }
        }


        private void ShowAddModal(object sender, EventArgs e)
        {
            AddCardModal addModal = new AddCardModal();

            addModal.MainCardAdd += (s, status) =>
            {
                AddMainCard(status);
                toiletStatusList.Add(status);
                repository.SaveAll(toiletStatusList);
            };

            addModal.ShowDialog();
        }

        private void ShowConnectModal(object sender, EventArgs e)
        {
            MainCard mc = sender as MainCard;

            if (mc != null)
            {
                string usedPort = toiletPorts.Where(p => p.ToiletId == mc.ToiletId)
                                             .Select(p => p.PortName)
                                             .FirstOrDefault(); 

                ConnectPortModal connectModal = new ConnectPortModal(mc.ToiletId, usedPort);
                connectModal.PortConnected += ConnectModal_PortConnected;

                connectModal.ShowDialog();
            }
        }

        private void ConnectModal_PortConnected(object sender, ConnectPortEventArgs args)
        {
            toiletPorts.RemoveAll(t => t.ToiletId == args.ToiletId);

            if (bltServices.Any(p => p.PortName == args.PortName))
            {
                MessageBox.Show($"포트 {args.PortName}은 이미 다른 화장실에 연결되어 있습니다.");
                return;
            }

            ToiletPort newMapping = new ToiletPort
            {
                ToiletId = args.ToiletId,
                PortName = args.PortName
            };

            toiletPorts.Add(newMapping);

            portRepository.SaveAll(toiletPorts);

            BltService bltService = new BltService(args.ToiletId, args.PortName);
            bltService.serialEvent += (s, data) =>
            {
                this.Invoke(new Action(() =>
                {
                    if (IdMapCard.ContainsKey(args.ToiletId))
                    {
                        IdMapCard[args.ToiletId].SetData(data);
                    }
                }));
            };
            bltServices.Add(bltService);
        }


        private void ShowDeleteModal(object sender, string toiletId)
        {
            if (!IdMapCard.ContainsKey(toiletId)) return;

            MainCard mc = IdMapCard[toiletId];

            DeleteCardModal deleteModal = new DeleteCardModal();

            deleteModal.MainCardDelete += (s, args) =>
            {
                tlpMainCard.Controls.Remove(mc.mainPanel);
                mc.mainPanel.Dispose();

                IdMapCard.Remove(toiletId);

                ToiletStatus ts = toiletStatusList.FirstOrDefault(x => x.ToiletId == toiletId);
                if (ts != null)
                {
                    toiletStatusList.Remove(ts);
                    repository.SaveAll(toiletStatusList);
                }

                BltService service = bltServices.FirstOrDefault(b => b.ToiletId == toiletId);
                if (service != null)
                {
                    service.Close();
                    bltServices.Remove(service);
                }

                toiletPorts.RemoveAll(p => p.ToiletId == toiletId);
                portRepository.SaveAll(toiletPorts);

                TableLayoutPanelCellPosition pos = tlpMainCard.GetPositionFromControl(mc.mainPanel);
                DummyCard dummyCard = new DummyCard();
                dummyCard.OpenAddModal += ShowAddModal;
                tlpMainCard.Controls.Add(dummyCard.dummyPanel, pos.Column, pos.Row);
            };

            deleteModal.ShowDialog();
        }

        private void DashbordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (BltService service in bltServices)
                service.Close();
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
