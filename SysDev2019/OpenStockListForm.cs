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
    public partial class OpenStockListForm : Form
    {
        private string employeeId;
        public OpenStockListForm(string employeeId)
        {
            InitializeComponent();

            this.employeeId = employeeId;
        }

        public void LogisticsManagerMenuForm()
        {
            Visible = false;
            LogisticsManagerMenuForm LogisticsManagerMenuForm = new LogisticsManagerMenuForm(employeeId);
            LogisticsManagerMenuForm.ShowDialog();
            Close();
        }

        public void OpenFilter_SearchForm()
        {
            Visible = false;
            Filter_SearchForm filter_SearchForm = new Filter_SearchForm(employeeId);
            filter_SearchForm.ShowDialog();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            LogisticsManagerMenuForm();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void InitializeStockList()
        {
            Task.Run(() =>
            {
                var orders = DatabaseInstance.StockTable.ToArray();
                
                try
                {
                    Invoke(new AsyncAction(() =>
                    {
                        dataGridView1.DataSource = orders;
                        var cols = dataGridView1.Columns;
                        cols.RemoveAt(cols.Count - 1);
                        dataGridView1.Columns[0].HeaderText = "在庫ID";

                        dataGridView1.Columns[1].HeaderText = "商品ID";
                        dataGridView1.Columns[2].HeaderText = "在庫数";
                        dataGridView1.Columns[3].HeaderText = "発注点";
                        dataGridView1.Columns[4].HeaderText = "発注点量";

                        

                        dataGridView1.Columns[0].ReadOnly = true;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;

                    }));
                }
                catch (ObjectDisposedException _)
                {
                    // ignore
                }
            });

        }

        delegate void AsyncAction();

        private void OpenStockListForm_Shown(object sender, EventArgs e)
        {
            InitializeStockList();
        }

        private void OpenStockListForm_Load(object sender, EventArgs e)
        {

        }
    }
}
