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
    public partial class OrderConfirmForm : Form
    {
        private string employeeId;
        public OrderConfirmForm(string employeeId)
        {
            InitializeComponent();
        }

        public void OpenFilter_SearchForm()
        {
            Visible = false;

            Filter_SearchForm filter_SearchForm = new Filter_SearchForm(employeeId);
            filter_SearchForm.ShowDialog();

            Close();
        }
        private void backButton_Click(object sender, EventArgs e)
        {

        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
