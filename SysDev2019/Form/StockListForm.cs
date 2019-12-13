using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using SysDev2019.DataModels;

namespace SysDev2019
{
    public partial class StockListForm : MetroForm
    {
        private readonly BindingList<Stock> bindingList = new BindingList<Stock>();
        private readonly string employeeId;
        public bool CloseFlag = true;
        private bool initializing;

        public StockListForm(string employeeId)
        {
            InitializeComponent();

            this.employeeId = employeeId;
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
                        initializing = true;
                        dataGridView1.DataSource = bindingList;
                        foreach (var stock in orders) bindingList.Add(stock);

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

                        initializing = false;
                    }));
                }
                catch (ObjectDisposedException _)
                {
                    // ignore
                }
            });
        }

        public void LogisticsManagerMenuForm()
        {
            Visible = false;
            var LogisticsManagerMenuForm = new LogisticsMenuForm(employeeId);
            LogisticsManagerMenuForm.ShowDialog();
            Close();
        }

        public void OpenFilter_SearchForm()
        {
            Visible = false;

            var filter_SearchForm = new FilterSearchForm(DatabaseInstance.StockTable.ToArray());
            if (filter_SearchForm.ShowDialog() == DialogResult.OK)
            {
                bindingList.Clear();
                foreach (var model in filter_SearchForm.Result)
                    if (model is Stock stock)
                        bindingList.Add(stock);
            }

            Visible = true;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            LogisticsManagerMenuForm();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void OpenStockListForm_Load(object sender, EventArgs e)
        {
        }

        private void OpenStockListForm_Shown(object sender, EventArgs e)
        {
            InitializeStockList();
        }

        private delegate void AsyncAction();
    }
}