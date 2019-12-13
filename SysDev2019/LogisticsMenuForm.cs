using System;
using System.Linq;
using System.Windows.Forms;

namespace SysDev2019
{
    public partial class LogisticsMenuForm : Form
    {
        private readonly string employeeId;

        public LogisticsMenuForm(string employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
            EmpName.Text = GetEmployeeName();
        }

        public void OpenOrderConfirmationForm()
        {
            Visible = false;
            var Openorder_Confirmation_Form = new OrderConfirmationForm(employeeId);
            Openorder_Confirmation_Form.ShowDialog();

            Visible = true;
        }

        public void OpenOrderingConfirmationForm()
        {
            Visible = false;
            var OpenOrderingConfirmationForm = new OrderingConfirmationForm(employeeId);
            OpenOrderingConfirmationForm.ShowDialog();

            Visible = true;
        }

        public void OpenOrderingForm()
        {
            Visible = false;

            var OpenOrderingForm = new OrderingForm(employeeId);
            OpenOrderingForm.ShowDialog();

            Visible = true;
        }

        public void OpenStockList()
        {
            Visible = false;

            var OpenStockListForm = new StockListForm(employeeId);
            OpenStockListForm.ShowDialog();

            Visible = true;
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
            var emp = DatabaseInstance.EmployeeTable.Where(e => e.EmployeeId == employeeId).First();
            return $"名前: {emp.Name}";
        }


        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void LogisticsManagerMenuForm_Load(object sender, EventArgs e)
        {
        }
    }
}