using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using SysDev2019.Dialog;

namespace SysDev2019
{
    public partial class LoginForm : MetroForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public void ErrorMessage(string message)
        {
            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void InvalidAuthMessage()
        {
            ErrorMessage("ログインに失敗しました。");
            Password.Text = "";
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

        public bool Login(string employeeId, string password)
        {
            return DatabaseInstance.EmployeeTable.Where(e => e.EmployeeId == employeeId).FirstOrDefault()?.Password ==
                   ToSha256(password);
        }

        public void NextForm(string employeeId)
        {
            if (IsSalesStaff(employeeId))
            {
                Visible = false;

                var staffMenuForm = new SalesStaffMenuForm(employeeId);
                staffMenuForm.ShowDialog();

                Visible = true;

                Activate();

                Employeenumber.Text = "";
                Password.Text = "";

                Employeenumber.Focus();
            }
            else if (IsLogisticsManager(employeeId))
            {
                Visible = false;

                var LogisticsManagerMenuForm = new LogisticsMenuForm(employeeId);
                LogisticsManagerMenuForm.ShowDialog();

                Visible = true;

                Activate();

                Employeenumber.Text = "";
                Password.Text = "";

                Employeenumber.Focus();
            }
            else
            {
                NotPermissionMessage();
            }
        }

        public void NotPermissionMessage()
        {
            ErrorMessage("権限がありません。");
            Employeenumber.Text = "";
            Password.Text = "";
            Employeenumber.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var empId = Employeenumber.Text;
            var pass = Password.Text;

            if (Login(empId, pass))
                NextForm(empId);
            else
                InvalidAuthMessage();
        }

        private void Employeenumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        private void Employeenumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter) e.Handled = true;
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // DatabaseInstance.Database.Dispose();
        }

        private void LoginForm_Shown(object sender, EventArgs e)
        {
            var dialog = new ProgressDialog();
            dialog.SetCallback(() =>
            {
                Task.Factory.StartNew(() =>
                {
                    var _ = DatabaseInstance.Database;
                    Invoke(new Action(() =>
                    {
                        dialog.DialogResult = DialogResult.OK;
                        dialog.Close();
                    }));
                });
            });
            dialog.ShowDialog();

            Activate();
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) button1_Click(this, EventArgs.Empty);
        }

        private void Password_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter) e.Handled = true;
        }

        private static string ToSha256(string password)
        {
            var sha256 = SHA256.Create();
            sha256.Initialize();

            var encoding = Encoding.UTF8;
            var hash = sha256.ComputeHash(encoding.GetBytes(password));
            return Convert.ToBase64String(hash);
        }
    }
}