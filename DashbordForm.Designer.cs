namespace CleanMonitor
{
    partial class DashbordForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.tlpHead = new System.Windows.Forms.TableLayoutPanel();
            this.pHead = new System.Windows.Forms.Panel();
            this.lblDist = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tlpMainCard = new System.Windows.Forms.TableLayoutPanel();
            this.tlpStatusCard = new System.Windows.Forms.TableLayoutPanel();
            this.tlpHead.SuspendLayout();
            this.pHead.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpHead
            // 
            this.tlpHead.BackColor = System.Drawing.Color.White;
            this.tlpHead.ColumnCount = 1;
            this.tlpHead.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpHead.Controls.Add(this.pHead, 0, 0);
            this.tlpHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpHead.Location = new System.Drawing.Point(0, 0);
            this.tlpHead.Name = "tlpHead";
            this.tlpHead.RowCount = 1;
            this.tlpHead.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpHead.Size = new System.Drawing.Size(1184, 58);
            this.tlpHead.TabIndex = 0;
            // 
            // pHead
            // 
            this.pHead.Controls.Add(this.lblDist);
            this.pHead.Controls.Add(this.label1);
            this.pHead.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pHead.Location = new System.Drawing.Point(3, 3);
            this.pHead.Name = "pHead";
            this.pHead.Size = new System.Drawing.Size(1178, 52);
            this.pHead.TabIndex = 0;
            // 
            // lblDist
            // 
            this.lblDist.AutoSize = true;
            this.lblDist.Location = new System.Drawing.Point(491, 31);
            this.lblDist.Name = "lblDist";
            this.lblDist.Size = new System.Drawing.Size(38, 12);
            this.lblDist.TabIndex = 2;
            this.lblDist.Text = "label2";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(29, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(25, 0, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Clean Monitor";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tlpMainCard
            // 
            this.tlpMainCard.ColumnCount = 5;
            this.tlpMainCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMainCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMainCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMainCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMainCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tlpMainCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMainCard.Location = new System.Drawing.Point(0, 58);
            this.tlpMainCard.Margin = new System.Windows.Forms.Padding(0);
            this.tlpMainCard.Name = "tlpMainCard";
            this.tlpMainCard.RowCount = 2;
            this.tlpMainCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpMainCard.Size = new System.Drawing.Size(1184, 323);
            this.tlpMainCard.TabIndex = 1;
            // 
            // tlpStatusCard
            // 
            this.tlpStatusCard.ColumnCount = 4;
            this.tlpStatusCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatusCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatusCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatusCard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStatusCard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tlpStatusCard.Location = new System.Drawing.Point(0, 381);
            this.tlpStatusCard.Margin = new System.Windows.Forms.Padding(0);
            this.tlpStatusCard.Name = "tlpStatusCard";
            this.tlpStatusCard.RowCount = 1;
            this.tlpStatusCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStatusCard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 180F));
            this.tlpStatusCard.Size = new System.Drawing.Size(1184, 180);
            this.tlpStatusCard.TabIndex = 0;
            // 
            // DashbordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.tlpMainCard);
            this.Controls.Add(this.tlpStatusCard);
            this.Controls.Add(this.tlpHead);
            this.Name = "DashbordForm";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DashbordForm_FormClosing);
            this.tlpHead.ResumeLayout(false);
            this.pHead.ResumeLayout(false);
            this.pHead.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tlpHead;
        private System.Windows.Forms.Panel pHead;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tlpMainCard;
        private System.Windows.Forms.TableLayoutPanel tlpStatusCard;
        private System.Windows.Forms.Label lblDist;
    }
}

