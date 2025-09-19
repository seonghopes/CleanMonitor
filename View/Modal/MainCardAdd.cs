using CleanMonitor.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CleanMonitor.View.Modal
{
    public partial class MainCardAdd : Form
    {
        public event EventHandler<ToiletStatus> MainCardAdded;
        public MainCardAdd()
        {
            InitializeComponent();

            this.AcceptButton = btnAdd;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string main = tbMainSection.Text.Trim();
            string sub = tbSubSection.Text.Trim();
            if (string.IsNullOrEmpty(main))
            {
                MessageBox.Show("Main Section을 입력하세요.");
                return;
            }

            ToiletStatus status = new ToiletStatus
            {
                ToiletId = Guid.NewGuid().ToString(),
                MainSection = main,
                SubSection = sub,
            };

            MainCardAdded?.Invoke(this, status);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
