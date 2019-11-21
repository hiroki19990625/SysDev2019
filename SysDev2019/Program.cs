using LogAdapter;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logger = NLog.Logger;

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
            var _ = DatabaseInstance.Database;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (sender, args) =>
            {
                MessageBox.Show(args.Exception.ToString(), "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Restart();
            };
            Application.Run(new LoginForm());
        }
    }
}