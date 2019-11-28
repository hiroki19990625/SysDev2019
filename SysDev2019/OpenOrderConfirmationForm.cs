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
    public partial class OpenOrder_Confirmation_Form : Form
    {
        private string pdfFile;
        private string employeeId;
        private bool OpenOrder;
        private bool initializing;

        private BindingList<Order> bindingList = new BindingList<Order>();
        public bool CloseFlag = true;

        public OpenOrder_Confirmation_Form(string employeeId)
        {
            InitializeComponent();

            this.employeeId = employeeId;
        }

        public void OpenFilter_SearchForm()
        {
            Visible = false;

            FilterSearchForm filter_SearchForm = new FilterSearchForm(DatabaseInstance.OrderTable.ToArray());
            if (filter_SearchForm.ShowDialog() == DialogResult.OK)
            {
                bindingList.Clear();
                foreach (DataModel model in filter_SearchForm.Result)
                {
                    if (model is Order order)
                        bindingList.Add(order);
                }
            }

            Visible = true;
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
                var orders = DatabaseInstance.OrderTable.Where(e => !e.ShipmentCompleted).ToArray();

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
                        dataGridView1.Columns[6].ReadOnly = true;


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

        private void OpenOrderConfirmationForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderList();
        }

        private void OpenOrder_Confirmation_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseInstance.OrderTable.Sync();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DatabaseInstance.OrderTable.Sync();
        }
    }

    internal class Confirmation
    {
    }
}