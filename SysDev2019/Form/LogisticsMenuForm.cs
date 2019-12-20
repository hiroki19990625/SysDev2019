using System;
using System.Linq;
using MetroFramework.Forms;

namespace SysDev2019
{
    public partial class LogisticsMenuForm : MetroForm
    {
        private readonly string _employeeId;

        public LogisticsMenuForm(string employeeId)
        {
            InitializeComponent();
            this._employeeId = employeeId;
            EmpName.Text = GetEmployeeName();
        }

        public void OpenOrderConfirmationForm()
        {
            Visible = false;
            var orderConfirmationForm = new OrderConfirmationForm();
            orderConfirmationForm.ShowDialog();

            Visible = true;

            Activate();
        }

        public void OpenOrderingConfirmationForm()
        {
            Visible = false;
            var orderingConfirmationForm = new OrderingConfirmationForm(_employeeId);
            orderingConfirmationForm.ShowDialog();

            Visible = true;

            Activate();
        }

        public void OpenOrderingForm()
        {
            Visible = false;

            var orderingForm = new OrderingForm(_employeeId);
            orderingForm.ShowDialog();

            Visible = true;

            Activate();
        }

        public void OpenStockList()
        {
            Visible = false;

            var stockListForm = new StockListForm(_employeeId);
            stockListForm.ShowDialog();

            Visible = true;

            Activate();
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

        private string GetEmployeeName()
        {
            var emp = DatabaseInstance.EmployeeTable.Where(e => e.EmployeeId == _employeeId).First();
            return $"名前: {emp.Name}";
        }


        private void LogisticsMenuForm_Shown(object sender, EventArgs e)
        {
            Activate();
        }
    }
}