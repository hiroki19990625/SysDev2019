using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Patagames.Pdf.Net.Controls.WinForms;
using SysDev2019.DataModels;

namespace SysDev2019
{
    public partial class PrintFilesDialog : Form
    {
        private (string, string)[] pdfFile;

        public PrintFilesDialog((string, string)[] pdfFile)
        {
            InitializeComponent();

            this.pdfFile = pdfFile;
            foreach ((string, string) tuple in pdfFile)
            {
                listBox1.Items.Add($"メーカー {tuple.Item2}");
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                Patagames.Pdf.Net.PdfDocument pdf =
                    Patagames.Pdf.Net.PdfDocument.Load(pdfFile[listBox1.SelectedIndex].Item1);
                PdfPrintDocument document = new PdfPrintDocument(pdf);

                PrintPreviewDialog dialog = new PrintPreviewDialog {Document = document};
                ToolStrip toolStrip = dialog.Controls[1] as ToolStrip;
                ToolStripButton printBtn = toolStrip.Items[0] as ToolStripButton;
                printBtn.Click += PrintBtnOnClick;
                printBtn.Click += (s, ev) => dialog.Close();
                toolStrip?.Items.RemoveAt(9);
                toolStrip?.Items.Add(new ToolStripButton("詳細設定", null, (s, ev) =>
                {
                    PrintDialog print = new PrintDialog {Document = document, UseEXDialog = true};
                    if (print.ShowDialog() == DialogResult.OK)
                    {
                        printBtn?.PerformClick();
                    }
                }));
                dialog.WindowState = FormWindowState.Maximized;
                dialog.ShowDialog();
                document.Dispose();
            }
        }

        private void PrintBtnOnClick(object sender, EventArgs ev)
        {
            var prod = DatabaseInstance.ProductTable.ToArray();
            var ordering = DatabaseInstance.OrderingTable.Where(e =>
                prod.First(e1 => e1.ProductId == e.ProductId).Manufacturer.ManufacturerName ==
                pdfFile[listBox1.SelectedIndex].Item2);

            foreach (Ordering od in ordering)
            {
                od.OrderingCompleted = true;
            }

            DatabaseInstance.OrderingTable.Sync();

            var l = pdfFile.ToList();
            l.RemoveAt(listBox1.SelectedIndex);
            pdfFile = l.ToArray();

            listBox1.Items.Clear();
            foreach ((string, string) tuple in pdfFile)
            {
                listBox1.Items.Add($"メーカー {tuple.Item2}");
            }
        }

        private void listBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) Keys.Enter)
            {
                listBox1_DoubleClick(this, EventArgs.Empty);
            }
        }
    }
}