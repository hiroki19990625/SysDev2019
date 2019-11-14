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
    public partial class OpenOrderingConfirmationForm : Form
    {
        private string employeeId;
        public OpenOrderingConfirmationForm(string employeeId)
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        private void OpenOrderingConfirmationForm_Load(object sender, EventArgs e)
        {

        }
        public void InitializeOrderingList()
        {
            Task.Run(() =>
            {
                var orders = DatabaseInstance.OrderingTable.Where(e => e.EmployeeId == employeeId).ToArray();
               
                try
                {
                    Invoke(new AsyncAction(() =>
                    {
                        dataGridView1.DataSource = orders;
                        var cols = dataGridView1.Columns;
                        cols.RemoveAt(cols.Count - 1);
                        cols.RemoveAt(cols.Count - 1);

                        dataGridView1.Columns[0].ReadOnly = true;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[3].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;

                        dataGridView1.Columns[0].HeaderText = "発注ID";
                        dataGridView1.Columns[1].HeaderText = "商品ID";
                        dataGridView1.Columns[2].HeaderText = "社員ID";
                        dataGridView1.Columns[3].HeaderText = "受注量";
                        dataGridView1.Columns[4].HeaderText = "受注日";
                        dataGridView1.Columns[5].HeaderText = "受注完了";
                        dataGridView1.Columns[6].HeaderText = "受け取り完了";
                    }));
                }
                catch (ObjectDisposedException _)
                {
                    // ignore
                }
            });

        }

        delegate void AsyncAction();

        private void OpenOrderingConfirmationForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderingList();
        }

        private void backButton_Click(object sender, EventArgs e)
        {

        }

        private void OpenOrderingConfirmationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseInstance.OrderingTable.Sync();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DatabaseInstance.OrderingTable.Sync();
        }
    }
}

