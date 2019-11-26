using ObjectDatabase;
using SysDev2019.DataModels;
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
        private bool openEntry;
        private bool initializing;

        private BindingList<Order> bindingList = new BindingList<Order>();

        public bool CloseFlag = true;
        private Order order;

        public OrderConfirmForm(string employeeId, bool openEntry = false)
        {
            InitializeComponent();

            this.employeeId = employeeId;
            this.openEntry = openEntry;
        }

        public void OpenFilter_SearchForm()
        {
            Visible = false;

            FilterSearchForm filter_SearchForm = new FilterSearchForm(DatabaseInstance.OrderTable.ToArray());
            filter_SearchForm.ShowDialog();

            Visible = true;
            bindingList.Clear();
            foreach (DataModel model in filter_SearchForm.Result)
            {
                if (model is Order order)
                    bindingList.Add(order);
            }


        }

        private void backButton_Click(object sender, EventArgs e)
        {
            OpenOrderEntryForm();
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

                OrderEntryForm form = new OrderEntryForm(employeeId);
                form.ShowDialog();

                Close();
            }
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void OrderConfirmForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderList();
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
                        initializing = true;
                        dataGridView1.DataSource = bindingList;
                        foreach (Order order in orders)
                        {
                            bindingList.Add(order);
                        }
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

        delegate void AsyncAction();

        private void OrderConfirmForm_Load(object sender, EventArgs e)
        {
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DatabaseInstance.OrderTable.Sync();
        }

        private void OrderConfirmForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseInstance.OrderTable.Sync();
        }
    }
}