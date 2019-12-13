﻿using SysDev2019.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SysDev2019.Dialog;

namespace SysDev2019
{
    public partial class OrderEntryForm : Form
    {
        private string employeeId;

        public OrderEntryForm(string employeeId)
        {
            InitializeComponent();

            count.Maximum = 100000;
            count.Minimum = 1;

            this.employeeId = employeeId;
        }

        public void OpenOrderConfirmForm()
        {
            Visible = false;

            OrderConfirmForm OrderConfirmForm = new OrderConfirmForm(employeeId, true);
            OrderConfirmForm.ShowDialog();

            if (OrderConfirmForm.CloseFlag)
                Close();
            else
                Visible = true;
        }

        public void InitializeProductList()
        {
            ProgressDialog dialog = new ProgressDialog();
            Action action = () => Task.Factory.StartNew(() =>
            {
                Invoke(new AsyncAction(() => product.BeginUpdate()));
                var products = DatabaseInstance.ProductTable.ToArray();
                foreach (var product in products)
                {
                    try
                    {
                        Invoke(new AsyncAction(() =>
                        {
                            this.product.Items.Add($"{product.ProductId}:{product.ProductName}");
                        }));
                    }
                    catch (ObjectDisposedException)
                    {
                        // ignore
                        break;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignore
                        break;
                    }
                }

                Invoke(new AsyncAction(() => product.EndUpdate()));
                Invoke(new AsyncAction(() => dialog.Close()));
            }, TaskCreationOptions.LongRunning);
            dialog.SetCallback(action);
            dialog.ShowDialog();
        }

        delegate void AsyncAction();

        private void orderEntryButton_Click(object sender, EventArgs e)
        {
            Order();

            product.SelectedIndex = -1;
            count.Value = 1;
        }

        private void orderConfirmButton_Click(object sender, EventArgs e)
        {
            Order();

            product.SelectedIndex = -1;
            count.Value = 1;
            OpenOrderConfirmForm();
        }

        private void product_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Order()
        {
            if (product.SelectedIndex != -1)
            {
                var prod = product.Text.Split(':');
                if (prod.Length > 0)
                {
                    var p = DatabaseInstance.ProductTable.Where(e => e.ProductId == prod[0]).FirstOrDefault();
                    if (p != null)
                    {
                        var stockCnt = DatabaseInstance.StockTable.Where(e => e.ProductId == p.ProductId)
                            .Sum(s => s.StockQuantity);
                        if (stockCnt < count.Value)
                        {
                            MessageBox.Show("在庫数が不足しています", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        var stocks = DatabaseInstance.StockTable.Where(e => e.ProductId == p.ProductId);
                        var diffs = (int) count.Value;
                        foreach (Stock stock in stocks)
                        {
                            var diffAny = stock.StockQuantity;
                            if (diffAny >= diffs)
                            {
                                stock.StockQuantity -= diffs;
                                break;
                            }

                            diffs -= diffAny;
                            stock.StockQuantity = 0;
                            if (diffs == 0)
                                break;
                        }

                        stockCnt = DatabaseInstance.StockTable.Where(e => e.ProductId == p.ProductId)
                                       .Sum(s => s.StockQuantity) + DatabaseInstance.OrderingTable
                                       .Where(e => e.ProductId == p.ProductId && !e.ReceiptComplete)
                                       .Sum(s => s.OrderingVolume);
                        var orderingPointCnt = DatabaseInstance.StockTable
                            .Where(e => e.ProductId == p.ProductId && e.ReorderPoint != -1).Sum(s => s.ReorderPoint);
                        if (orderingPointCnt >= stockCnt)
                        {
                            var reorderCnt = DatabaseInstance.StockTable
                                .Where(e => e.ProductId == p.ProductId && e.OrderQuantity != -1)
                                .Sum(s => s.OrderQuantity);
                            var reorder = reorderCnt - stockCnt;
                            var ordering = new Ordering
                            {
                                OrderingId = Guid.NewGuid().ToString(),
                                ProductId = p.ProductId,
                                EmployeeId = "3000",
                                OrderingVolume = reorder,
                                OrderingDate = DateTime.Now.ToString()
                            };

                            DatabaseInstance.OrderingTable.Insert(ordering);
                            DatabaseInstance.OrderingTable.Sync();
                        }


                        var order = new Order
                        {
                            OrderId = Guid.NewGuid().ToString(),
                            EmployeeId = employeeId,
                            ProductId = p.ProductId,
                            OrderVolume = (int) count.Value,
                            OrderDate = DateTime.Now.ToString()
                        };

                        DatabaseInstance.OrderTable.Insert(order);
                        DatabaseInstance.OrderTable.Sync();

                        MessageBox.Show("注文を完了しました", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        product.Focus();
                    }
                }
            }
        }


        private void OrderEntryForm_Shown(object sender, EventArgs e)
        {
            InitializeProductList();
        }

        private void count_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                orderEntryButton_Click(this, EventArgs.Empty);
            }
        }

        private void product_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void product_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void count_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                e.Handled = true;
            }
        }
    }
}