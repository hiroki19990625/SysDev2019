using System;
using System.Linq;
using System.Windows.Forms;

namespace SysDev2019
{
    public partial class SalesStaffMenuForm : Form
    {
        private readonly string employeeId;

        public SalesStaffMenuForm(string employeeId)
        {
            InitializeComponent();

            this.employeeId = employeeId;
            EmpName.Text = GetEmployeeName();
        }

        public void OpenOrderConfirmForm()
        {
            Visible = false;

            var OrderConfirmForm = new OrderConfirmForm(employeeId);
            OrderConfirmForm.ShowDialog();

            Visible = true;
        }

        public void OpenOrderEntryForm()
        {
            Visible = false;

            var OrderEntryForm = new OrderEntryForm(employeeId);
            OrderEntryForm.ShowDialog();

            Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenOrderEntryForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenOrderConfirmForm();
        }

        private string GetEmployeeName()
        {
            var emp = DatabaseInstance.EmployeeTable.Where(e => e.EmployeeId == employeeId).First();
            return $"名前: {emp.Name}";
        }

        private void SalesStaffMenuForm_Load(object sender, EventArgs e)
        {
        }
    }
}