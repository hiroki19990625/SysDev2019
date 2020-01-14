using System;
using System.ComponentModel;
using System.Drawing;
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

                        dataGridView1.SuspendLayout();
                        foreach (var order in orders) bindingList.Add(order);

                        dataGridView1.Columns[0].HeaderText = "受注ID";
                        dataGridView1.Columns[1].HeaderText = "社員ID";
                        dataGridView1.Columns[2].HeaderText = "社員名";
                        dataGridView1.Columns[3].HeaderText = "部署名";
                        dataGridView1.Columns[4].HeaderText = "商品ID";
                        dataGridView1.Columns[5].HeaderText = "商品名";
                        dataGridView1.Columns[6].HeaderText = "単価";
                        dataGridView1.Columns[7].HeaderText = "メーカー名";
                        dataGridView1.Columns[8].HeaderText = "受注量";
                        dataGridView1.Columns[9].HeaderText = "受注日";
                        dataGridView1.Columns[10].HeaderText = "受注完了";
                        dataGridView1.Columns[11].HeaderText = "注文キャンセル";
                        dataGridView1.Columns[12].HeaderText = "出荷完了";

                        dataGridView1.Columns[1].Visible = false;
                        dataGridView1.Columns[4].Visible = false;
                        dataGridView1.Columns[13].Visible = false;
                        dataGridView1.Columns[14].Visible = false;

                        dataGridView1.Columns[0].ReadOnly = true;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[3].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;
                        dataGridView1.Columns[5].ReadOnly = true;
                        dataGridView1.Columns[6].ReadOnly = true;
                        dataGridView1.Columns[7].ReadOnly = true;
                        dataGridView1.Columns[8].ReadOnly = true;
                        dataGridView1.Columns[9].ReadOnly = true;
                        dataGridView1.Columns[10].ReadOnly = true;
                        dataGridView1.Columns[12].ReadOnly = true;

                        dataGridView1.ResumeLayout();

                        initializing = false;
                    }));
                }
                catch (ObjectDisposedException)
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

            Activate();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            OpenOrderEntryForm();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!initializing)
            {
                DatabaseInstance.OrderTable.Sync();
                if (e.ColumnIndex == 11)
                {
                    var check = (bool) dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    var orderId = (string) dataGridView1[0, e.RowIndex].Value;
                    var order = DatabaseInstance.OrderTable.Where(el => el.OrderId == orderId)
                        .FirstOrDefault();
                    if (order != null)
                        if (check && MessageBox.Show("キャンセルしますか?", "情報", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            var stock = new Stock
                            {
                                StockId = Guid.NewGuid().ToString(),
                                ProductId = order.ProductId,
                                StockQuantity = order.OrderVolume,
                                ReorderPoint = -1,
                                OrderQuantity = -1
                            };
                            DatabaseInstance.StockTable.Insert(stock);
                            DatabaseInstance.StockTable.Sync();
                            DatabaseInstance.UpdateUnion();
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

        private void OrderConfirmForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderList();

            Activate();
        }

        private delegate void AsyncAction();

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.Handled = true;
                e.PaintBackground(e.ClipBounds, false);
                string text = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                Font font = dataGridView1.ColumnHeadersDefaultCellStyle.Font;
                Brush foreBrush = new SolidBrush(
                    dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor);
                StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(text, font, foreBrush, e.CellBounds, sf);
            }
        }
    }
}