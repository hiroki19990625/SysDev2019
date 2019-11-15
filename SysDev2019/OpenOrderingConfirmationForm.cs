﻿using SysDev2019.DataModels;
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
        private bool OpenOrdering;
        private bool initializing;

        public bool CloseFlag = true;
        public OpenOrderingConfirmationForm(string employeeId, bool OpenOrdering = false)
        {
            InitializeComponent();

            this.employeeId = employeeId;
            this.OpenOrdering = OpenOrdering;
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
                        initializing = true;
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
                        dataGridView1.Columns[3].HeaderText = "発注量";
                        dataGridView1.Columns[4].HeaderText = "発注日";
                        dataGridView1.Columns[5].HeaderText = "発注完了";
                        dataGridView1.Columns[6].HeaderText = "受け取り完了";

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

        private void OpenOrderingConfirmationForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderingList();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            OpenOrderingForm();
        }

        private void OpenOrderingForm()
        {
            if (OpenOrdering)
            {
                CloseFlag = false;
                Close();
            }
            else
            {
                Visible = false;

                OpenOrderingForm form = new OpenOrderingForm(employeeId);
                form.ShowDialog();

                Close();
            }
        }

        private void OpenOrderingConfirmationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseInstance.OrderingTable.Sync();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!initializing) { 
                DatabaseInstance.OrderingTable.Sync();
                if (e.ColumnIndex == 6)
                {
                    var check = (bool)dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    var orderingId = (string)dataGridView1[0, e.RowIndex].Value;
                    var ordering = DatabaseInstance.OrderingTable.Where(el => el.OrderingId == orderingId).FirstOrDefault();
                    if (ordering != null)
                    {
                        if (check && MessageBox.Show("在庫を自動的に追加しますか?", "情報", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            var stock = new Stock
                            {
                                StockId = Guid.NewGuid().ToString(),
                                ProductId = ordering.ProductId,
                                StockQuantity = ordering.OrderingVolume,
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
    }
}

