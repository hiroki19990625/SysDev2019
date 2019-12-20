using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using SysDev2019.DataModels;

namespace SysDev2019
{
    public partial class OrderConfirmationForm : MetroForm
    {
        private readonly BindingList<Order> _bindingList = new BindingList<Order>();
        public bool CloseFlag = true;
        private string _employeeId;

        public OrderConfirmationForm(string employeeId)
        {
            InitializeComponent();

            this._employeeId = employeeId;
        }

        public OrderConfirmationForm()
        {
            InitializeComponent();
        }

        public void InitializeOrderList()
        {
            Task.Run(() =>
            {
                var orders = DatabaseInstance.OrderTable.Where(e => !e.ShipmentCompleted).ToArray();

                try
                {
                    Invoke(new AsyncAction(() =>
                    {
                        dataGridView1.DataSource = _bindingList;

                        foreach (var order in orders) _bindingList.Add(order);

                        var cols = dataGridView1.Columns;
                        cols.RemoveAt(cols.Count - 1);
                        cols.RemoveAt(cols.Count - 1);

                        dataGridView1.Columns[0].ReadOnly = true;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[3].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;
                        dataGridView1.Columns[6].ReadOnly = true;

                        dataGridView1.Columns[0].HeaderText = "受注ID";
                        dataGridView1.Columns[1].HeaderText = "社員ID";
                        dataGridView1.Columns[2].HeaderText = "商品ID";
                        dataGridView1.Columns[3].HeaderText = "受注量";
                        dataGridView1.Columns[4].HeaderText = "受注日";
                        dataGridView1.Columns[5].HeaderText = "受注完了";
                        dataGridView1.Columns[6].HeaderText = "注文キャンセル";
                        dataGridView1.Columns[7].HeaderText = "出荷完了";
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
                _bindingList.Clear();
                foreach (var model in filter_SearchForm.Result)
                    if (model is Order order)
                        _bindingList.Add(order);
            }

            Visible = true;

            Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DatabaseInstance.OrderTable.Sync();
        }

        private void OpenOrder_Confirmation_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseInstance.OrderTable.Sync();
        }

        private void OpenOrderConfirmationForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderList();

            Activate();
        }

        private delegate void AsyncAction();
    }
}