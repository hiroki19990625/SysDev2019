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
            return DatabaseInstance.EmployeeTable.Where(e => e.DepartmentId == "11" && e.EmployeeId == employeeId).FirstOrDefault() != null; 
        }

        public bool IsSalesStaff(string employeeId)
        {
            return DatabaseInstance.EmployeeTable.Where(e => e.DepartmentId == "10" && e.EmployeeId == employeeId).FirstOrDefault() != null;
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
                NextForm();
            
            }
            else
            {
                InvalidAuthMessage();
            }
        }
    }
}