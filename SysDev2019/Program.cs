using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysDev2019
{
    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (sender, args) =>
            {
                MessageBox.Show("再起動します", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            };
            Application.Run(new LoginForm());
        }
    }
}