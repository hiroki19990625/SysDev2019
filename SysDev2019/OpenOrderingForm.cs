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
    public partial class OpenOrderingForm : Form
    {
        private string employeeId;

        public OpenOrderingForm(string employeeId)
        {
            InitializeComponent();

            count.Maximum = 100000;
            count.Minimum = 1;

            this.employeeId = employeeId;
        }

        public void OpenOrderingConfirmationForm()
        {
            Visible = false;

            OpenOrderingConfirmationForm OpenOrderingConfirmationForm =
                new OpenOrderingConfirmationForm(employeeId, true);
            OpenOrderingConfirmationForm.ShowDialog();

            if (OpenOrderingConfirmationForm.CloseFlag)
                Close();
            else
                Visible = true;
        }

        public void InitializeManufacturerList()
        {
            Task.Factory.StartNew(() =>
            {
                var manufacturers = DatabaseInstance.ManufacturerTable.ToArray();
                foreach (var manufacturer in manufacturers)
                {
                    try
                    {
                        Invoke(new AsyncAction(() =>
                        {
                            this.Manufacturer.Items.Add(
                                $"{manufacturer.ManufacturerId}:{manufacturer.ManufacturerName}");
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
            }, TaskCreationOptions.LongRunning);
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
                        break;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignore
                        break;
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }

        delegate void AsyncAction();

        private void label1_Click(object sender, EventArgs e)
        {
        }


        private void orderingButton_Click(object sender, EventArgs e)
        {
            Ordering();

            product.SelectedIndex = -1;
            count.Value = 1;
        }

        private void orderingConfirmButton_Click(object sender, EventArgs e)
        {
            Ordering();
            product.SelectedIndex = -1;
            count.Value = 1;

            OpenOrderingConfirmationForm();
        }

        private void Ordering()
        {
            if (product.SelectedIndex != -1)
            {
                var prod = product.Text.Split(':');

                if (prod.Length > 0)
                {
                    var p = DatabaseInstance.ProductTable.Where(e => e.ProductId == prod[0]).FirstOrDefault();
                    if (p != null)
                    {
                        var ordering = new Ordering
                        {
                            OrderingId = Guid.NewGuid().ToString(),
                            ProductId = p.ProductId,
                            EmployeeId = employeeId,
                            OrderingVolume = (int) count.Value,
                            OrderingDate = DateTime.Now.ToString()
                        };

                        DatabaseInstance.OrderingTable.Insert(ordering);
                        DatabaseInstance.OrderingTable.Sync();

                        MessageBox.Show("発注を完了しました", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        Manufacturer.Text = "";
                        product.Text = "";
                        count.Text = "";

                        Manufacturer.Focus();
                    }
                }
            }
        }

        private void OpenOrderingFrom_Shown(object sender, EventArgs e)
        {
            InitializeProductList();
            InitializeManufacturerList();
        }

        private void Manufacturer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void product_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void count_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                orderingButton_Click(this, EventArgs.Empty);
            }
        }

        private void Manufacturer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                e.Handled = true;
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