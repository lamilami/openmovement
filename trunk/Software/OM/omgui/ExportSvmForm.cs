using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OmGui
{
    public partial class ExportSvmForm : Form
    {
        public ExportSvmForm()
        {
            InitializeComponent();
            comboBoxFilter.SelectedIndex = 0;
            comboBoxMode.SelectedIndex = 0;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonResample_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ExportSvmForm_Load(object sender, EventArgs e)
        {
        }
    }
}
