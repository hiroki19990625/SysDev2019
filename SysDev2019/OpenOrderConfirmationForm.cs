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
    public partial class OpenOrder_Confirmation_Form : Form
    {
        private string employeeId;
        public OpenOrder_Confirmation_Form(string employeeId)
        {
            InitializeComponent();

            this.employeeId = employeeId;
        }

        public void OpenFilter_SearchForm()
        {
            Visible = false;

            Filter_SearchForm filter_SearchForm = new Filter_SearchForm(employeeId);
            filter_SearchForm.ShowDialog();

            Close();
        }

        private void OpenOrder_Confirmatioin_Form_Shown(object sender, EventArgs e)
        {
            InitializeOrderList();
        }

        public OpenOrder_Confirmation_Form()
        {
            InitializeComponent();
        }

        private void OpenOrder_Confirmation_Form_Load(object sender, EventArgs e)
        {

        }

        private void backButton_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        public void InitializeOrderList()
        {
            Task.Run(() =>
            {
                var orders = DatabaseInstance.OrderTable.Where(e => e.EmployeeId == employeeId).ToArray();

                try
                {
                    Invoke(new AsyncAction(() =>
                    {
                        dataGridView1.DataSource = orders;
                        var cols = dataGridView1.Columns;
                        cols.RemoveAt(cols.Count - 1);
                        cols.RemoveAt(cols.Count - 1);
                    }));
                }
                catch (ObjectDisposedException _)
                {
                    // ignore
                }
            });

        }

        delegate void AsyncAction();

        private void OpenOrderConfirmationForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderList();
        }
    }
}
