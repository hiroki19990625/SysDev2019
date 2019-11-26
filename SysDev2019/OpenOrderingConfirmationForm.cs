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
using ObjectDatabase;
using Patagames.Pdf.Net.Controls.WinForms;
using Image = iText.Layout.Element.Image;

namespace SysDev2019
{
    public partial class OpenOrderingConfirmationForm : Form
    {
        private (string, string)[] pdfFile;
        private string employeeId;
        private bool OpenOrdering;
        private bool initializing;

        private BindingList<Ordering> bindingList = new BindingList<Ordering>();

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

            FilterSearchForm filter_SearchForm = new FilterSearchForm(DatabaseInstance.OrderingTable.ToArray());
            filter_SearchForm.ShowDialog();

            Visible = true;

            bindingList.Clear();
            foreach (DataModel model in filter_SearchForm.Result)
            {
                if (model is Ordering ordering)
                    bindingList.Add(ordering);
            }
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
                var orders = DatabaseInstance.OrderingTable.Where(e =>
                    (e.EmployeeId == employeeId || e.EmployeeId == "3000") && !e.ReceiptComplete).ToArray();
                try
                {
                    Invoke(new AsyncAction(() =>
                    {
                        initializing = true;
                        dataGridView1.DataSource = bindingList;
                        foreach (Ordering ordering in orders)
                        {
                            bindingList.Add(ordering);
                        }

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

            PrintFilesDialog dialog = new PrintFilesDialog(pdfFile);
            dialog.ShowDialog();
        }

        public (string, string)[] CreateDocument()
        {
            if (!Directory.Exists("Docs"))
                Directory.CreateDirectory("Docs");

            var prod = DatabaseInstance.ProductTable.ToArray();
            var manifs = DatabaseInstance.OrderingTable.Where(e =>
                !e.OrderingCompleted && (e.EmployeeId == employeeId || e.EmployeeId == "3000")).GroupBy(e =>
                prod.First(e1 => e1.ProductId == e.ProductId).Manufacturer.ManufacturerName);

            var files = new List<(string, string)>();
            string folder = "Docs/" + DateTime.Now.Ticks;
            foreach (IGrouping<string, Ordering> manif in manifs)
            {
                Directory.CreateDirectory(folder);

                string file = $"{folder}/帳票_({manif.Key}).pdf";
                var stream = new MemoryStream();
                PdfWriter writer = new PdfWriter(stream);

                PdfFont font = PdfFontFactory.CreateFont("c:\\windows\\fonts\\msgothic.ttc,0", "Identity-H");

                PdfDocument pdf = new PdfDocument(writer);
                Document d = new Document(pdf);

                var sum = manif.Sum(e => prod.First(e1 => e1.ProductId == e.ProductId).UnitPrice);

                d.Add(new Paragraph($"発注書作成日： {DateTime.Today:yyyy/MM/dd}").SetFont(font).SetFontSize(15)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
                d.Add(new Paragraph($"注文番号：{DateTime.Now.Ticks.ToString()}").SetFont(font).SetFontSize(15)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT));
                d.Add(new Paragraph("発注書").SetFont(font).SetFontSize(30)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));
                d.Add(new Paragraph($"{manif.Key} 様").SetFont(font).SetFontSize(22)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT));
                d.Add(new Paragraph($"発注金額 {sum} 円").SetFont(font).SetFontSize(25)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT));
                d.Add(new Paragraph($"下記の通り発注致します。").SetFont(font).SetFontSize(15)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT));

                Table table = new Table(4);
                table.SetFont(font).SetFontSize(15);
                table.AddHeaderCell(new Paragraph("商品名").SetFont(font).SetFontSize(15));
                table.AddHeaderCell(new Paragraph("単価 (円)").SetFont(font).SetFontSize(15));
                table.AddHeaderCell(new Paragraph("個数").SetFont(font).SetFontSize(15));
                table.AddHeaderCell(new Paragraph("合計 (円)").SetFont(font).SetFontSize(15));

                foreach (Ordering odin in manif)
                {
                    var pd = prod.First(e => e.ProductId == odin.ProductId);
                    table.AddCell(new Paragraph(pd.ProductName)
                        .SetFont(font).SetFontSize(12));
                    table.AddCell(new Paragraph(pd.UnitPrice.ToString())
                        .SetFont(font).SetFontSize(12));
                    table.AddCell(new Paragraph(odin.OrderingVolume.ToString())
                        .SetFont(font).SetFontSize(12));
                    table.AddCell(new Paragraph((odin.OrderingVolume * pd.UnitPrice).ToString())
                        .SetFont(font).SetFontSize(12));
                }

                d.Add(table);

                pdf.Close();
                d.Close();
                writer.Close();

                File.WriteAllBytes(file, stream.ToArray());

                stream.Close();

                files.Add((file, manif.Key));
            }

            return files.ToArray();
        }
    }
}