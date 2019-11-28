using SysDev2019.DataModels;
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
    public partial class OpenOrderingForm : Form
    {
        private string employeeId;
        private readonly CancellationTokenSource _tokenSource = new CancellationTokenSource();
        private readonly CancellationTokenSource _tokenSource2 = new CancellationTokenSource();
        private CancellationTokenSource _tokenSource3 = new CancellationTokenSource();

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
                try
                {
                    var manufacturers = DatabaseInstance.ManufacturerTable.ToArray();
                    foreach (var manufacturer in manufacturers)
                    {
                        _tokenSource.Token.ThrowIfCancellationRequested();
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
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
            }, _tokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }


        public void InitializeProductList()
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var products = DatabaseInstance.ProductTable.ToArray();
                    foreach (var product in products)
                    {
                        _tokenSource2.Token.ThrowIfCancellationRequested();
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
                }
                catch (OperationCanceledException)
                {
                    // ignore
                }
            }, _tokenSource2.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        delegate void AsyncAction();

        private void label1_Click(object sender, EventArgs e)
        {
        }


        private void orderingButton_Click(object sender, EventArgs e)
        {
            Ordering();

            product.SelectedIndex = -1;
            Manufacturer.SelectedIndex = -1;
            count.Value = 1;
        }

        private void orderingConfirmButton_Click(object sender, EventArgs e)
        {
            Ordering();
            product.SelectedIndex = -1;
            Manufacturer.SelectedIndex = -1;
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

        private void Manufacturer_SelectedIndexChanged(object sender, EventArgs ev)
        {
            if (Manufacturer.SelectedIndex != -1)
            {
                var manu = Manufacturer.Text.Split(':');

                if (manu.Length > 0)
                {
                    _tokenSource2.Cancel();
                    _tokenSource3.Cancel();

                    _tokenSource3.Dispose();
                    _tokenSource3 = new CancellationTokenSource();

                    product.Items.Clear();

                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            var products = DatabaseInstance.ProductTable.Where(e => e.ManufacturerId == manu[0]);
                            foreach (var product in products)
                            {
                                _tokenSource3.Token.ThrowIfCancellationRequested();
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
                        }
                        catch (OperationCanceledException)
                        {
                            // ignore
                        }
                    }, _tokenSource3.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                }
            }
        }
    }
}