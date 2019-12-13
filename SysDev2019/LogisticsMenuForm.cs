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
    public partial class LogisticsMenuForm : Form
    {
        private string employeeId;

        public LogisticsMenuForm(string employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            EmpName.Text = GetEmployeeName();
        }

        public void OpenStockList()
        {
            Visible = false;

            StockListForm OpenStockListForm = new StockListForm(employeeId);
            OpenStockListForm.ShowDialog();

            Visible = true;
        }

        public void OpenOrderingForm()
        {
            Visible = false;

            OrderingForm OpenOrderingForm = new OrderingForm(employeeId);
            OpenOrderingForm.ShowDialog();

            Visible = true;
        }

        public void OpenOrderingConfirmationForm()
        {
            Visible = false;
            OrderingConfirmationForm OpenOrderingConfirmationForm = new OrderingConfirmationForm(employeeId);
            OpenOrderingConfirmationForm.ShowDialog();

            Visible = true;
        }

        public void OpenOrderConfirmationForm()
        {
            Visible = false;
            OrderConfirmationForm Openorder_Confirmation_Form = new OrderConfirmationForm(employeeId);
            Openorder_Confirmation_Form.ShowDialog();

            Visible = true;
        }

        private string GetEmployeeName()
        {
            var emp = DatabaseInstance.EmployeeTable.Where(e => e.EmployeeId == employeeId).First();
            return $"名前: {emp.Name}";
        }


        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenStockList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenOrderingForm();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenOrderingConfirmationForm();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenOrderConfirmationForm();
        }

        private void LogisticsManagerMenuForm_Load(object sender, EventArgs e)
        {
        }
    }
}