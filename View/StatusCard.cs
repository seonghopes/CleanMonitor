using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMonitor
{
    class StatusCard : UserControl
    {
        public TableLayoutPanel statusPanel;
        private PictureBox statusPic;
        private Label statusCnt;
        private Label statusText;

        private int cnt = 0; 
        public StatusCard(string Text, Color color, Image pic = null) 
        {
            InitUI(color,pic);
            statusText.Text = Text;
        }

        private void InitUI(Color color,Image pic)
        {
            this.statusPanel = new System.Windows.Forms.TableLayoutPanel();
            this.statusPic = new System.Windows.Forms.PictureBox();
            this.statusCnt = new System.Windows.Forms.Label();
            this.statusText = new System.Windows.Forms.Label();

            statusPanel.BackColor = System.Drawing.Color.White;
            statusPanel.Dock = DockStyle.Fill;
            statusPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            statusPanel.ColumnCount = 1;
            statusPanel.Controls.Add(statusPic, 0, 1);
            statusPanel.Controls.Add(statusCnt, 0, 2);
            statusPanel.Controls.Add(statusText, 0, 3);
            statusPanel.RowCount = 5;
            statusPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            statusPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            statusPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            statusPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            statusPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));

            statusPic.Anchor = System.Windows.Forms.AnchorStyles.None;
            statusPic.BackColor = color; 
            statusPic.Size = new System.Drawing.Size(50, 50);
            statusPic.TabStop = false;

            statusPic.Paint += (s, e) =>
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddEllipse(0, 0, statusPic.Width - 1, statusPic.Height - 1);
                statusPic.Region = new Region(path);
            };

            if (pic != null)
            {
                statusPic.Image = pic;
                statusPic.SizeMode = PictureBoxSizeMode.CenterImage;
            }

            statusCnt.AutoSize = true;
            statusCnt.Dock = System.Windows.Forms.DockStyle.Fill;
            statusCnt.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            statusCnt.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            statusCnt.Text = "0";

            statusText.AutoSize = true;
            statusText.Dock = System.Windows.Forms.DockStyle.Top;
            statusText.Font = new System.Drawing.Font("맑은 고딕", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            statusText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        }


        public void Increment()
        {
            cnt++;
            UpdateLabel();
        }

        public void Decrement()
        {
            if (cnt > 0)
                cnt--;
            UpdateLabel();
        }

        private void UpdateLabel()
        {
            statusCnt.Text = cnt.ToString();
        }

        public int GetCount()
        {
            return cnt;
        }

        public void SetCount(int val)
        {
            if (val == 0) statusCnt.Text = "0";
            statusCnt.Text = val.ToString();
        }
    }
}
