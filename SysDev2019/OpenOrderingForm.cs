using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysDev2019
{
    public partial class OpenOrderingForm : Form
    {
        private string employeeId;
        public OpenOrderingForm(string employeeId)
        {
            InitializeComponent();
        }

        public void OpenOrderingConfirmationForm()
        {
            Visible = false;

            OpenOrderingConfirmationForm OpenOrderingConfirmationForm = new OpenOrderingConfirmationForm(employeeId);
            OpenOrderingConfirmationForm.ShowDialog();

            Visible = true;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void orderingConfirmButton_Click(object sender, EventArgs e)
        {
            OpenOrderingConfirmationForm();
        }

        private void orderingButton_Click(object sender, EventArgs e)
        {

        }
    }
}
