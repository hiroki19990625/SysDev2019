using SysDev2019.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BinaryIO;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using Patagames.Pdf.Net.Controls.WinForms;
using Image = iText.Layout.Element.Image;

namespace SysDev2019
{
    public partial class OpenOrderingConfirmationForm : Form
    {
        private string pdfFile;
        private string employeeId;
        private bool OpenOrdering;
        private bool initializing;

        public bool CloseFlag = true;

        static OpenOrderingConfirmationForm()
        {
        }

        public OpenOrderingConfirmationForm(string employeeId, bool OpenOrdering = false)
        {
            InitializeComponent();

            this.employeeId = employeeId;
            this.OpenOrdering = OpenOrdering;
        }

        public void OpenFilter_SearchForm()
        {
            Visible = false;

            Filter_SearchForm filter_SearchForm = new Filter_SearchForm(employeeId);
            filter_SearchForm.ShowDialog();

            Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        private void OpenOrderingConfirmationForm_Load(object sender, EventArgs e)
        {
        }

        public void InitializeOrderingList()
        {
            Task.Run(() =>
            {
                var orders = DatabaseInstance.OrderingTable
                    .Where(e => e.EmployeeId == employeeId || e.EmployeeId == "3000").ToArray();

                try
                {
                    Invoke(new AsyncAction(() =>
                    {
                        initializing = true;
                        dataGridView1.DataSource = orders;
                        var cols = dataGridView1.Columns;
                        cols.RemoveAt(cols.Count - 1);
                        cols.RemoveAt(cols.Count - 1);

                        dataGridView1.Columns[0].ReadOnly = true;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[3].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;

                        dataGridView1.Columns[0].HeaderText = "発注ID";
                        dataGridView1.Columns[1].HeaderText = "商品ID";
                        dataGridView1.Columns[2].HeaderText = "社員ID";
                        dataGridView1.Columns[3].HeaderText = "発注量";
                        dataGridView1.Columns[4].HeaderText = "発注日";
                        dataGridView1.Columns[5].HeaderText = "発注完了";
                        dataGridView1.Columns[6].HeaderText = "受け取り完了";

                        initializing = false;
                    }));
                }
                catch (ObjectDisposedException _)
                {
                    // ignore
                }
            });
        }

        delegate void AsyncAction();

        private void OpenOrderingConfirmationForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderingList();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            OpenOrderingForm();
        }

        private void OpenOrderingForm()
        {
            if (OpenOrdering)
            {
                CloseFlag = false;
                Close();
            }
            else
            {
                Visible = false;

                OpenOrderingForm form = new OpenOrderingForm(employeeId);
                form.ShowDialog();

                Close();
            }
        }

        private void OpenOrderingConfirmationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseInstance.OrderingTable.Sync();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!initializing)
            {
                DatabaseInstance.OrderingTable.Sync();
                if (e.ColumnIndex == 6)
                {
                    var check = (bool) dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    var orderingId = (string) dataGridView1[0, e.RowIndex].Value;
                    var ordering = DatabaseInstance.OrderingTable.Where(el => el.OrderingId == orderingId)
                        .FirstOrDefault();
                    if (ordering != null)
                    {
                        if (check && MessageBox.Show("在庫を自動的に追加しますか?", "情報", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            var stock = new Stock
                            {
                                StockId = Guid.NewGuid().ToString(),
                                ProductId = ordering.ProductId,
                                StockQuantity = ordering.OrderingVolume,
                                ReorderPoint = -1,
                                OrderQuantity = -1
                            };
                            DatabaseInstance.StockTable.Insert(stock);
                            DatabaseInstance.StockTable.Sync();
                        }
                    }
                }
            }
        }

        private void printing_Click(object sender, EventArgs e)
        {
            pdfFile = CreateDocument();

            Patagames.Pdf.Net.PdfDocument pdf = Patagames.Pdf.Net.PdfDocument.Load(pdfFile);
            PdfPrintDocument document = new PdfPrintDocument(pdf);

            PrintPreviewDialog dialog = new PrintPreviewDialog {Document = document};
            ToolStrip toolStrip = dialog.Controls[1] as ToolStrip;
            toolStrip?.Items.RemoveAt(9);
            toolStrip?.Items.Add(new ToolStripButton("詳細設定", null, (s, ev) =>
            {
                PrintDialog print = new PrintDialog {Document = document, UseEXDialog = true};
                if (print.ShowDialog() == DialogResult.OK)
                {
                    ToolStripButton button = toolStrip.Items[0] as ToolStripButton;
                    button?.PerformClick();
                }
            }));
            dialog.WindowState = FormWindowState.Maximized;
            dialog.ShowDialog();
            document.Dispose();
        }

        public string CreateDocument()
        {
            if (!Directory.Exists("Docs"))
                Directory.CreateDirectory("Docs");

            string file = $"Docs/帳票_{DateTime.Now.Ticks}_(メーカー).pdf";
            var stream = new MemoryStream();
            PdfWriter writer = new PdfWriter(stream);

            PdfFont font = PdfFontFactory.CreateFont("c:\\windows\\fonts\\msgothic.ttc,0", "Identity-H");

            PdfDocument pdf = new PdfDocument(writer);
            Document d = new Document(pdf);

            d.Add(new Paragraph("シベリア送り～").SetFont(font).SetFontSize(20));
            d.Add(new Paragraph(DateTime.Now.Ticks.ToString()).SetFont(font).SetFontSize(25));

            Table table = new Table(dataGridView1.ColumnCount - 2);
            table.SetFont(font).SetFontSize(15);
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if (col.Index == 5)
                    break;
                Cell c = new Cell();
                c.SetWidth(col.Width);
                c.Add(new Paragraph(col.HeaderText));
                table.AddHeaderCell(c);
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (column.Index == 5)
                        break;
                    table.AddCell(row.Cells[column.Index].Value.ToString());
                }
            }

            d.Add(table);

            pdf.Close();
            d.Close();
            writer.Close();

            File.WriteAllBytes(file, stream.ToArray());

            stream.Close();

            return file;
        }
    }
}