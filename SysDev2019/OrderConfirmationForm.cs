﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysDev2019.DataModels;

namespace SysDev2019
{
    public partial class OrderConfirmationForm : Form
    {
        private readonly BindingList<Order> bindingList = new BindingList<Order>();
        public bool CloseFlag = true;
        private string employeeId;
        private bool initializing;
        private bool OpenOrder;
        private string pdfFile;

        public OrderConfirmationForm(string employeeId)
        {
            InitializeComponent();

            this.employeeId = employeeId;
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
        }

        private delegate void AsyncAction();
    }
}