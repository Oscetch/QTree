using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QTree.TestTool.Dialogs
{
    public partial class SizeDialog : Form
    {
        public int SelectedWidth { get; private set; }
        public int SelectedHeight { get; private set; }

        public SizeDialog()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            SelectedWidth = (int)widthNumeric.Value;
            SelectedHeight = (int)heightNumeric.Value;

            DialogResult = DialogResult.OK;
        }
    }
}
