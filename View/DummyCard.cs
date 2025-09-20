using CleanMonitor.View.Modal;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMonitor
{
    internal class DummyCard : UserControl
    {
        public TableLayoutPanel dummyPanel;

        private Button addBtn;
        public event EventHandler OpenAddModal;

        public const string DUMMY_TAG = "Dummy";

        public DummyCard()
        {
            InitUI();
          
            dummyPanel.Tag = DUMMY_TAG;
        }

        private void InitUI()
        {
            this.dummyPanel = new System.Windows.Forms.TableLayoutPanel();
            this.addBtn = new System.Windows.Forms.Button();



            dummyPanel.BackColor = System.Drawing.Color.WhiteSmoke;
            dummyPanel.Dock = System.Windows.Forms.DockStyle.Fill;

            dummyPanel.ColumnCount = 3;
            dummyPanel.RowCount = 3;
            dummyPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            dummyPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            dummyPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            dummyPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            dummyPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            dummyPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
            dummyPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            addBtn.Dock = DockStyle.None;
            addBtn.Anchor = AnchorStyles.None;
            addBtn.Size = new Size(100, 40);
            addBtn.Text = "+ 추가";
            addBtn.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))); ;
            addBtn.Cursor = Cursors.Hand;
            addBtn.FlatStyle = FlatStyle.Flat;
            addBtn.BackColor = Color.LightGray;
            addBtn.FlatAppearance.BorderSize = 1;
            addBtn.FlatAppearance.BorderColor = Color.LightGray;

            addBtn.MouseEnter += (s, e) =>
            {
                addBtn.FlatAppearance.BorderSize = 1;
                addBtn.FlatAppearance.BorderColor = Color.WhiteSmoke;
            };

            addBtn.MouseLeave += (s, e) =>
            {
                addBtn.FlatAppearance.BorderSize = 1;
                addBtn.FlatAppearance.BorderColor = Color.LightGray;
            };

            addBtn.Click += (s, e) =>
            {
                OpenAddModal?.Invoke(this, EventArgs.Empty); 
            };


            dummyPanel.Controls.Add(addBtn, 1, 1);

          
        }
    }
}
