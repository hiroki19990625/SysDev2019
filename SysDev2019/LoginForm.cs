using LogAdapter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysDev2019
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            var _ = DatabaseInstance.Database;
            _.AddLogEvent(OnLog);
        }

        public bool Login(string employeeId, string password)
        {
            return DatabaseInstance.EmployeeTable.Where(e => e.EmployeeId == employeeId).FirstOrDefault()?.Password ==
                   ToSha256(password);
        }

        public bool IsLogisticsManager(string employeeId)
        {
            return DatabaseInstance.EmployeeTable.Where(e => e.DepartmentId == "11" && e.EmployeeId == employeeId)
                       .FirstOrDefault() != null;
        }

        public bool IsSalesStaff(string employeeId)
        {
            return DatabaseInstance.EmployeeTable.Where(e => e.DepartmentId == "10" && e.EmployeeId == employeeId)
                       .FirstOrDefault() != null;
        }

        public void NextForm(string employeeId)
        {
            if (IsSalesStaff(employeeId))
            {
                Visible = false;

                SalesStaffMenuForm staffMenuForm = new SalesStaffMenuForm(employeeId);
                staffMenuForm.ShowDialog();

                Visible = true;

                Employeenumber.Text = "";
                Password.Text = "";

                Employeenumber.Focus();
            }
            else if (IsLogisticsManager(employeeId))
            {
                Visible = false;

                LogisticsManagerMenuForm LogisticsManagerMenuForm = new LogisticsManagerMenuForm(employeeId);
                LogisticsManagerMenuForm.ShowDialog();

                Visible = true;

                Employeenumber.Text = "";
                Password.Text = "";

                Employeenumber.Focus();
            }
            else
            {
                NotPermissionMessage();
            }
        }

        public void InvalidAuthMessage()
        {
            ErrorMessage("ログインに失敗しました。");
            Password.Text = "";
        }

        public void ErrorMessage(string message)
        {
            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void NotPermissionMessage()
        {
            ErrorMessage("権限がありません。");
            Employeenumber.Text = "";
            Password.Text = "";
            Employeenumber.Focus();
        }

        static string ToSha256(string password)
        {
            SHA256 sha256 = SHA256.Create();
            sha256.Initialize();

            Encoding encoding = Encoding.UTF8;
            byte[] hash = sha256.ComputeHash(encoding.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string empId = Employeenumber.Text;
            string pass = Password.Text;

            if (Login(empId, pass))
            {
                NextForm(empId);
            }
            else
            {
                InvalidAuthMessage();
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // DatabaseInstance.Database.Dispose();
        }

        private void OnLog(ILogMessage msg)
        {
             MessageBox.Show(msg.Data.ToString());
        }

        private void Employeenumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(this, EventArgs.Empty);
            }
        }

        private void Employeenumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        }

        private void Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
            }
        
        }
    }
}