using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMonitor
{
    class MainCard : UserControl
    {
        public TableLayoutPanel mainPanel;

        private PictureBox mainPic;
        
        private Label mainSection;
        private Label subSection;

        private Label statusCir1;
        private Label statusCir2;
        private Label statusCir3;

        private Label statusText1;
        private Label statusText2;
        private Label statusText3;

        private Label updateTime;

        public MainCard() 
        {
            InitUI();
        }

        private void InitUI()
        {
            this.mainPanel = new System.Windows.Forms.TableLayoutPanel();

            this.mainPic = new System.Windows.Forms.PictureBox();

            this.mainSection = new System.Windows.Forms.Label();
            this.subSection = new System.Windows.Forms.Label();

            this.statusCir1 = new System.Windows.Forms.Label();
            this.statusCir2 = new System.Windows.Forms.Label();
            this.statusCir3 = new System.Windows.Forms.Label();

            this.statusText1 = new System.Windows.Forms.Label();
            this.statusText2 = new System.Windows.Forms.Label();
            this.statusText3 = new System.Windows.Forms.Label();

            this.updateTime = new System.Windows.Forms.Label();


            mainPanel.BackColor = System.Drawing.Color.White;
            mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            mainPanel.ColumnCount = 3;
            mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            mainPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            mainPanel.Controls.Add(this.mainPic, 0, 1);
            mainPanel.Controls.Add(this.statusCir1, 0, 5);
            mainPanel.Controls.Add(this.statusCir2, 1, 5);
            mainPanel.Controls.Add(this.statusCir3, 2, 5);
            mainPanel.Controls.Add(this.mainSection, 0, 2);
            mainPanel.Controls.Add(this.subSection, 0, 3);
            mainPanel.Controls.Add(this.statusText1, 0, 6);
            mainPanel.Controls.Add(this.statusText2, 1, 6);
            mainPanel.Controls.Add(this.statusText3, 2, 6);
            mainPanel.Controls.Add(this.updateTime, 1, 7);
            mainPanel.Location = new System.Drawing.Point(3, 3);
            mainPanel.RowCount = 8;
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 30F));
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13F));
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 7F));
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5F));
            mainPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 15F));
            mainPanel.Size = new System.Drawing.Size(230, 300);

            mainPanel.SetColumnSpan(mainPic, 3);
            mainPanel.SetColumnSpan(mainSection, 3);
            mainPanel.SetColumnSpan(subSection, 3);

            mainPic.Anchor = System.Windows.Forms.AnchorStyles.None;
            mainPic.BackColor = System.Drawing.Color.LightGray;
            mainPic.Size = new System.Drawing.Size(150, 100);

            mainSection.AutoSize = true;
            mainSection.Dock = System.Windows.Forms.DockStyle.Fill;
            mainSection.Text = "MAIN SECTION";                                       //
            mainSection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            mainSection.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            subSection.AutoSize = true;
            subSection.Dock = System.Windows.Forms.DockStyle.Top;
            subSection.Text = "sub section";                                            //
            subSection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            subSection.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));

            statusCir1.Anchor = System.Windows.Forms.AnchorStyles.None;
            statusCir1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            statusCir1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            statusCir1.ForeColor = System.Drawing.Color.Red;
            statusCir1.Size = new System.Drawing.Size(25, 25);
            statusCir1.Text = "0";                                                        //
            statusCir1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            statusCir2.Anchor = System.Windows.Forms.AnchorStyles.None;
            statusCir2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            statusCir2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            statusCir2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            statusCir2.Size = new System.Drawing.Size(25, 25);
            statusCir2.Text = "0";                                                       //
            statusCir2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            statusCir3.Anchor = System.Windows.Forms.AnchorStyles.None;
            statusCir3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            statusCir3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            statusCir3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            statusCir3.Size = new System.Drawing.Size(25, 25);
            statusCir3.Text = "3";                                                      //
            statusCir3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;


            statusText1.AutoSize = true;
            statusText1.Dock = System.Windows.Forms.DockStyle.Top;
            statusText1.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            statusText1.ForeColor = System.Drawing.Color.Gray;
            statusText1.Text = "긴급";                                                      //
            statusText1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            statusText2.AutoSize = true;
            statusText2.Dock = System.Windows.Forms.DockStyle.Top;
            statusText2.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            statusText2.ForeColor = System.Drawing.Color.Gray;
            statusText2.Text = "주의";                                                      //
            statusText2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            statusText3.AutoSize = true;
            statusText3.Dock = System.Windows.Forms.DockStyle.Top;
            statusText3.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            statusText3.ForeColor = System.Drawing.Color.Gray;
            statusText3.Text = "정상";                                                      //
            statusText3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            updateTime.Dock = System.Windows.Forms.DockStyle.Fill;
            updateTime.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            updateTime.ForeColor = System.Drawing.Color.Gray;
            updateTime.Text = "n분전";                                                      //
            updateTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        }

        public void SetWarnCnt(int cnt) {
            statusText1.Text = cnt.ToString();
        }
    }
}
