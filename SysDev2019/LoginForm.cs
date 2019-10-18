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
        }

        public bool Login(string employeeId, string password)
        {
            return DatabaseInstance.EmployeeTable.Where(e => e.EmployeeId == employeeId).FirstOrDefault()?.Password ==
                   ToSha256(password);
        }

        public bool IsLogisticsManager(string employeeId)
        {
            return DatabaseInstance.EmployeeTable.Where(e => e.EmployeeId == employeeId).FirstOrDefault()
                       ?.DepartmentId == "20";
        }

        public bool IsSalesStaff(string employeeId)
        {
            return DatabaseInstance.EmployeeTable.Where(e => e.EmployeeId == employeeId).FirstOrDefault()
                       ?.DepartmentId == "10";
        }

        public void NextForm()
        {
            throw new System.NotImplementedException();
        }

        public void InvalidAuthMessage()
        {
            ErrorMessage("ログインに失敗しました。");
        }

        public void ErrorMessage(string message)
        {
            MessageBox.Show(message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void NotPermissionMessage()
        {
            ErrorMessage("権限がありません。");
        }

        private string ToSha256(string password)
        {
            SHA256 sha256 = SHA256.Create();
            sha256.Initialize();

            Encoding encoding = Encoding.UTF8;
            return encoding.GetString(sha256.ComputeHash(encoding.GetBytes(password)));
        }
    }
}