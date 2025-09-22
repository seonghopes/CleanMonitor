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
    public partial class DeleteCardModal : Form
    {
        public event EventHandler MainCardDelete;

        public DeleteCardModal()
        {
            InitializeComponent();
            this.CancelButton = btnCancel;
        }

        private void btnDelCard_Click(object sender, EventArgs e)
        {
            MainCardDelete?.Invoke(this, EventArgs.Empty); 
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
