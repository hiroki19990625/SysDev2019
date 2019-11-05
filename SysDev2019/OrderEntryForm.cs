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
    public partial class OrderEntryForm : Form
    {
        private string employeeId;
        public OrderEntryForm(string employeeId)
        {
            InitializeComponent();

            count.Maximum = 100000;
            count.Minimum = 1;

            this.employeeId = employeeId;
            InitializeProductList();
        }

        public void OpenOrderConfirmForm()
        {

        }

        public void InitializeProductList()
        {
            var products = DatabaseInstance.ProductTable.ToArray();
            foreach (var product in products)
            {
                this.product.Items.Add($"{product.ProductId}:{product.ProductName}");
            }
        }

        private void orderEntryButton_Click(object sender, EventArgs e)
        {
            Order();
        }

        private void orderConfirmButton_Click(object sender, EventArgs e)
        {
            Order();

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
                    }
                }
            }
        }
    }
}
