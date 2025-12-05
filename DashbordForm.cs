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
        private Timer dashboardTimer; 

        private StatusCard allStatusCard;
        private StatusCard sirenStatusCard;
        private StatusCard wranStatusCard;
        private StatusCard checkStatusCard;


        private ToiletRepository repository = new ToiletRepository();
        private List<ToiletStatus> toiletStatusList = new List<ToiletStatus>();
        private Dictionary<string, MainCard> IdMapCard = new Dictionary<string, MainCard>();

        private PortRepository portRepository = new PortRepository();
        private List<ToiletPort> toiletPorts = new List<ToiletPort>();

        private List<BltService> bltServices = new List<BltService>();

        private MqttService mqttService;

        public DashbordForm()
        {
            InitializeComponent();

            FormSizeInit();
            AddDummyCard();
            AddStuatusCard();

            LoadMainCardList();

            StartDashboardTimer();

            InitMqtt();
        }

        private async void InitMqtt()
        {
            try
            {
                mqttService = new MqttService();

                // MQTT 메시지 수신 이벤트
                mqttService.dataReceived += (s, msg) =>
                {
                    string topic = msg.topic;
                    string message = msg.message;

                    // UI 스레드에서 안전하게 처리
                    this.BeginInvoke(new Action(() =>
                    {
                        // topic = toilet/{id}
                        string toiletId = topic.Replace("toilet/", "");

                        if (IdMapCard.ContainsKey(toiletId))
                        {
                            IdMapCard[toiletId].SetData(message);
                        }
                    }));
                };

                // 브로커 연결
                await mqttService.ConnectAsync("", 1883);

                // 모든 화장실 카드 topic 구독
                foreach (var status in toiletStatusList)
                {
                    string topic = $"toilet/{status.ToiletId}";
                    await mqttService.SubscribeAsync(topic);
                }

                Console.WriteLine("MQTT 연결 및 구독 완료");
            }
            catch (Exception ex)
            {
                MessageBox.Show("MQTT 초기화 오류: " + ex.Message);
            }
        }

        private void StartDashboardTimer()
        {
            // UI 스레드에서 동작하는 Windows Forms Timer
            dashboardTimer = new Timer();
            dashboardTimer.Interval = 1000; // 1초마다 갱신
            dashboardTimer.Tick += DashboardTimer_Tick;
            dashboardTimer.Start();
        }

        private void DashboardTimer_Tick(object sender, EventArgs e)
        {
            int totalNormal = 0;
            int totalWarning = 0;
            int totalCritical = 0;

            // MainCard 개별 센서 상태 집계
            foreach (MainCard mc in IdMapCard.Values)
            {
                totalNormal += mc.SensorsCount(SensorLevel.Normal);
                totalWarning += mc.SensorsCount(SensorLevel.Warning);
                totalCritical += mc.SensorsCount(SensorLevel.Critical);
            }

            // StatusCard UI 갱신
            checkStatusCard.SetCount(totalNormal);
            wranStatusCard.SetCount(totalWarning);
            sirenStatusCard.SetCount(totalCritical);
        }

        private void FormSizeInit()
        {
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = new Size(1280, 960);
            this.MaximumSize = new Size(1920, 1080);
        }

        public void AddStuatusCard()
        {
            allStatusCard = new StatusCard("총 화장실", Color.LightSteelBlue, Properties.Resources.toilet);
            sirenStatusCard = new StatusCard("교체 필요", Color.LightPink, Properties.Resources.siren);
            wranStatusCard = new StatusCard("주의 상태", Color.LightGoldenrodYellow, Properties.Resources.warn);
            checkStatusCard = new StatusCard("정상 상태", Color.LightGreen, Properties.Resources.check);
            tlpStatusCard.Controls.Add(allStatusCard.statusPanel, 0, 0);
            tlpStatusCard.Controls.Add(sirenStatusCard.statusPanel, 1, 0);
            tlpStatusCard.Controls.Add(wranStatusCard.statusPanel, 2, 0);
            tlpStatusCard.Controls.Add(checkStatusCard.statusPanel, 3, 0);
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
            Task.Run(() =>
            {
                List<ToiletStatus> tss = repository.LoadAll();
                List<ToiletPort> tps = portRepository.LoadAll();

                this.BeginInvoke(new Action(() =>
                {
                    toiletStatusList = tss;
                    toiletPorts = tps;

                    foreach (ToiletStatus status in toiletStatusList)
                    {
                        AddMainCard(status);
                    }

                    foreach (ToiletPort tp in toiletPorts)
                    {
                        if (IdMapCard.ContainsKey(tp.ToiletId))
                        {
                            BltService bltService = new BltService(tp.ToiletId, tp.PortName);
                            bltService.serialEvent += (s, data) =>
                            {
                                // 이벤트 호출 스레드가 UI 스레드가 아님
                                // BeginInvoke (비동기),  Invoke (동기)
                                this.BeginInvoke(new Action(() =>
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

                }));

            });

        }


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

                        allStatusCard.Increment();
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

            if (toiletPorts.Any(p => p.PortName == args.PortName && p.ToiletId != args.ToiletId))
            {
                MessageBox.Show($"포트 {args.PortName}은 이미 다른 화장실에 연결되어 있습니다.");
                return;
            }

            toiletPorts.RemoveAll(t => t.ToiletId == args.ToiletId);


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
                this.BeginInvoke(new Action(() =>
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

                allStatusCard.Decrement();
            };

            deleteModal.ShowDialog();
        }

        private void DashbordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (BltService service in bltServices)
                service.Close();
        }

        
    }
}
