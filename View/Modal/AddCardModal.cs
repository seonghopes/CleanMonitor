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
    public partial class AddCardModal : Form
    {
        public event EventHandler<ToiletStatus> MainCardAdd;
        public AddCardModal()
        {
            InitializeComponent();

            this.AcceptButton = btnAdd; 
            this.CancelButton = btnCancel;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string toiletId = tbToiletId.Text.Trim();
            string main = tbMainSection.Text.Trim();
            string sub = tbSubSection.Text.Trim();
            if (string.IsNullOrEmpty(toiletId))
            {
                MessageBox.Show("장소 ID를 입력하세요.");
                return;
            }
            if (string.IsNullOrEmpty(main))
            {
                MessageBox.Show("Main Section을 입력하세요.");
                return;
            }

            ToiletStatus status = new ToiletStatus
            {
                ToiletId = toiletId,
                MainSection = main,
                SubSection = sub,
            };

            MainCardAdd?.Invoke(this, status);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
