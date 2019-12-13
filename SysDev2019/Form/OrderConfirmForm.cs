using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using SysDev2019.DataModels;

namespace SysDev2019
{
    public partial class OrderConfirmForm : MetroForm
    {
        private readonly BindingList<Order> bindingList = new BindingList<Order>();
        private readonly string employeeId;
        private readonly bool openEntry;

        public bool CloseFlag = true;
        private bool initializing;

        public OrderConfirmForm(string employeeId, bool openEntry = false)
        {
            InitializeComponent();

            this.employeeId = employeeId;
            this.openEntry = openEntry;
        }

        public void InitializeOrderList()
        {
            Task.Run(() =>
            {
                var orders = DatabaseInstance.OrderTable.Where(e => e.EmployeeId == employeeId && !e.CancelOrder)
                    .ToArray();

                try
                {
                    Invoke(new AsyncAction(() =>
                    {
                        initializing = true;
                        dataGridView1.DataSource = bindingList;
                        foreach (var order in orders) bindingList.Add(order);

                        var cols = dataGridView1.Columns;
                        cols.RemoveAt(cols.Count - 1);
                        cols.RemoveAt(cols.Count - 1);

                        dataGridView1.Columns[0].ReadOnly = true;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[3].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;
                        dataGridView1.Columns[5].ReadOnly = true;
                        dataGridView1.Columns[7].ReadOnly = true;


                        dataGridView1.Columns[0].HeaderText = "受注ID";
                        dataGridView1.Columns[1].HeaderText = "社員ID";
                        dataGridView1.Columns[2].HeaderText = "商品ID";
                        dataGridView1.Columns[3].HeaderText = "受注量";
                        dataGridView1.Columns[4].HeaderText = "受注日";
                        dataGridView1.Columns[5].HeaderText = "受注完了";
                        dataGridView1.Columns[6].HeaderText = "注文キャンセル";
                        dataGridView1.Columns[7].HeaderText = "出荷完了";

                        initializing = false;
                    }));
                }
                catch (ObjectDisposedException _)
                {
                    // ignore
                }
            });
        }

        public void OpenFilter_SearchForm()
        {
            Visible = false;

            var filter_SearchForm = new FilterSearchForm(DatabaseInstance.OrderTable.ToArray());
            if (filter_SearchForm.ShowDialog() == DialogResult.OK)
            {
                bindingList.Clear();
                foreach (var model in filter_SearchForm.Result)
                    if (model is Order order)
                        bindingList.Add(order);
            }

            Visible = true;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            OpenOrderEntryForm();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!initializing)
            {
                DatabaseInstance.OrderTable.Sync();
                if (e.ColumnIndex == 6)
                {
                    var check = (bool) dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    var orderId = (string) dataGridView1[0, e.RowIndex].Value;
                    var order = DatabaseInstance.OrderTable.Where(el => el.OrderId == orderId)
                        .FirstOrDefault();
                    if (order != null)
                    {
                        if (check && MessageBox.Show("キャンセルしますか?", "情報", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            Stock stock = new Stock
                            {
                                StockId = Guid.NewGuid().ToString(),
                                ProductId = order.ProductId,
                                StockQuantity = order.OrderVolume,
                                ReorderPoint = -1,
                                OrderQuantity = -1
                            };
                            DatabaseInstance.StockTable.Insert(stock);
                            DatabaseInstance.StockTable.Sync();
                        }
                    }
                }
            }
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        private void OpenOrderEntryForm()
        {
            if (openEntry)
            {
                CloseFlag = false;
                Close();
            }
            else
            {
                Visible = false;

                var form = new OrderEntryForm(employeeId);
                form.ShowDialog();

                Close();
            }
        }

        private void OrderConfirmForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseInstance.OrderTable.Sync();
        }

        private void OrderConfirmForm_Load(object sender, EventArgs e)
        {
        }

        private void OrderConfirmForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderList();
        }

        private delegate void AsyncAction();
    }
}