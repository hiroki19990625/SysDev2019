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
            Task.Factory.StartNew(() =>
            {
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
                    }
                    catch (InvalidOperationException)
                    {
                        // ignore
                    }
                }
            }, TaskCreationOptions.LongRunning);
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

                        product.Text = "";
                        count.Text = "";

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