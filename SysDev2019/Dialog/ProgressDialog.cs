using System;
using System.Windows.Forms;

namespace SysDev2019.Dialog
{
    public partial class ProgressDialog : Form
    {
        private Action _action;

        public ProgressDialog()
        {
            InitializeComponent();

            progressBar1.MarqueeAnimationSpeed = 50;
        }

        public void SetCallback(Action action)
        {
            _action = action;
        }

        private void LoadViewDialog_Shown(object sender, EventArgs e)
        {
            _action();
        }
    }
}