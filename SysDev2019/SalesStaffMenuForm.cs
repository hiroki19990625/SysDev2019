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
    public partial class SalesStaffMenuForm : Form
    {
        private string employeeId;

        public SalesStaffMenuForm(string employeeId)
        {
            InitializeComponent();

            this.employeeId = employeeId;
            EmpName.Text = GetEmployeeName();
        }

        public void OpenOrderEntryForm()
        {

        }

        public void OpenOrderConfirmForm()
        {

        }

        private string GetEmployeeName()
        {
            var emp = DatabaseInstance.EmployeeTable.Where(e => e.EmployeeId == employeeId).First();
            return $"名前: {emp.Name}";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenOrderEntryForm();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenOrderConfirmForm();

        }
    }
}
