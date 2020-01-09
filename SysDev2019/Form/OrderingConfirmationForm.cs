using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using MetroFramework.Forms;
using SysDev2019.DataModels;
using SysDev2019.Dialog;

namespace SysDev2019
{
    public partial class OrderingConfirmationForm : MetroForm
    {
        private readonly BindingList<Ordering> bindingList = new BindingList<Ordering>();
        private readonly string employeeId;
        private readonly bool OpenOrdering;

        public bool CloseFlag = true;
        private bool initializing;
        private (string, string)[] pdfFile;

        public OrderingConfirmationForm(string employeeId, bool OpenOrdering = false)
        {
            InitializeComponent();

            this.employeeId = employeeId;
            this.OpenOrdering = OpenOrdering;
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
            var folder = "Docs/" + DateTime.Now.Ticks;
            foreach (var manif in manifs)
            {
                Directory.CreateDirectory(folder);

                var file = $"{folder}/帳票_({manif.Key}).pdf";
                var stream = new MemoryStream();
                var writer = new PdfWriter(stream);

                var font = PdfFontFactory.CreateFont("c:\\windows\\fonts\\msgothic.ttc,0", "Identity-H");

                var pdf = new PdfDocument(writer);
                var d = new Document(pdf);

                var sum = manif.Sum(e => prod.First(e1 => e1.ProductId == e.ProductId).UnitPrice * e.OrderingVolume);

                d.Add(new Paragraph($"発注書作成日： {DateTime.Today:yyyy/MM/dd}").SetFont(font).SetFontSize(15)
                    .SetTextAlignment(TextAlignment.RIGHT));
                d.Add(new Paragraph($"注文番号：{DateTime.Now.Ticks.ToString()}").SetFont(font).SetFontSize(15)
                    .SetTextAlignment(TextAlignment.RIGHT));
                d.Add(new Paragraph("発注書").SetFont(font).SetFontSize(30)
                    .SetTextAlignment(TextAlignment.CENTER));
                d.Add(new Paragraph($"{manif.Key} 様").SetFont(font).SetFontSize(22)
                    .SetUnderline()
                    .SetTextAlignment(TextAlignment.RIGHT));
                d.Add(new Paragraph());
                d.Add(new Paragraph());
                d.Add(new Paragraph());
                d.Add(new Paragraph($"発注金額 {sum * 1.1:C0}-")
                    .SetUnderline()
                    .SetFont(font)
                    .SetFontSize(25)
                    .SetTextAlignment(TextAlignment.LEFT));
                d.Add(new Paragraph("下記の通り発注致します。").SetFont(font).SetFontSize(15)
                    .SetTextAlignment(TextAlignment.LEFT));

                var table = new Table(4);
                table.SetFont(font).SetFontSize(15).SetWidth(UnitValue.CreatePercentValue(100));
                table.AddHeaderCell(new Paragraph("商品名").SetFont(font).SetFontSize(15));
                table.AddHeaderCell(new Paragraph("個数").SetFont(font).SetFontSize(15).SetWidth(75));
                table.AddHeaderCell(new Paragraph("単価").SetFont(font).SetFontSize(15).SetWidth(75));
                table.AddHeaderCell(new Paragraph("合計").SetFont(font).SetFontSize(15).SetWidth(75));

                foreach (var odin in manif)
                {
                    var pd = prod.First(e => e.ProductId == odin.ProductId);
                    table.AddCell(new Paragraph(pd.ProductName)
                        .SetFont(font).SetFontSize(12));
                    table.AddCell(new Paragraph(odin.OrderingVolume.ToString())
                        .SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Paragraph($"{pd.UnitPrice:C0}")
                        .SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT));
                    table.AddCell(new Paragraph($"{odin.OrderingVolume * pd.UnitPrice:C0}")
                        .SetFont(font).SetFontSize(12).SetTextAlignment(TextAlignment.RIGHT));
                }

                table.AddCell(new Paragraph("")
                    .SetFont(font).SetFontSize(15));
                table.AddCell(new Paragraph("")
                    .SetFont(font).SetFontSize(15));
                table.AddCell(new Paragraph("小計")
                    .SetFont(font).SetFontSize(15).SetTextAlignment(TextAlignment.CENTER));
                table.AddCell(new Paragraph($"{sum:C0}")
                    .SetFont(font).SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT));

                table.AddCell(new Paragraph("")
                    .SetFont(font).SetFontSize(15));
                table.AddCell(new Paragraph("")
                    .SetFont(font).SetFontSize(15));
                table.AddCell(new Paragraph("消費税")
                    .SetFont(font).SetFontSize(15).SetTextAlignment(TextAlignment.CENTER));
                table.AddCell(new Paragraph($"{sum * 0.1:C0}")
                    .SetFont(font).SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT));


                table.AddCell(new Paragraph("")
                    .SetFont(font).SetFontSize(15));
                table.AddCell(new Paragraph("")
                    .SetFont(font).SetFontSize(15));
                table.AddCell(new Paragraph("合計")
                    .SetFont(font).SetFontSize(15).SetTextAlignment(TextAlignment.CENTER));
                table.AddCell(new Paragraph($"{sum * 1.1:C0}-")
                    .SetFont(font).SetFontSize(15).SetTextAlignment(TextAlignment.RIGHT));

                d.Add(table);

                var max = pdf.GetNumberOfPages();
                for (var i = 1; i <= max; i++)
                    d.ShowTextAligned(new Paragraph($"ページ {i} / {max}").SetFont(font), 559, 820, i,
                        TextAlignment.CENTER,
                        VerticalAlignment.BOTTOM, 0);

                pdf.Close();
                d.Close();
                writer.Close();

                File.WriteAllBytes(file, stream.ToArray());

                stream.Close();

                files.Add((file, manif.Key));
            }

            return files.ToArray();
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
                        foreach (var ordering in orders) bindingList.Add(ordering);

                        dataGridView1.Columns[0].HeaderText = "受注ID";
                        dataGridView1.Columns[1].HeaderText = "社員ID";
                        dataGridView1.Columns[2].HeaderText = "社員名";
                        dataGridView1.Columns[3].HeaderText = "部署名";
                        dataGridView1.Columns[4].HeaderText = "商品ID";
                        dataGridView1.Columns[5].HeaderText = "商品名";
                        dataGridView1.Columns[6].HeaderText = "単価";
                        dataGridView1.Columns[7].HeaderText = "メーカー名";
                        dataGridView1.Columns[8].HeaderText = "受注量";
                        dataGridView1.Columns[9].HeaderText = "受注日";
                        dataGridView1.Columns[10].HeaderText = "発注完了";
                        dataGridView1.Columns[11].HeaderText = "受け取り完了";

                        dataGridView1.Columns[1].Visible = false;
                        dataGridView1.Columns[4].Visible = false;
                        dataGridView1.Columns[12].Visible = false;
                        dataGridView1.Columns[13].Visible = false;

                        dataGridView1.Columns[0].ReadOnly = true;
                        dataGridView1.Columns[1].ReadOnly = true;
                        dataGridView1.Columns[2].ReadOnly = true;
                        dataGridView1.Columns[3].ReadOnly = true;
                        dataGridView1.Columns[4].ReadOnly = true;
                        dataGridView1.Columns[5].ReadOnly = true;
                        dataGridView1.Columns[6].ReadOnly = true;
                        dataGridView1.Columns[7].ReadOnly = true;
                        dataGridView1.Columns[8].ReadOnly = true;
                        dataGridView1.Columns[9].ReadOnly = true;

                        initializing = false;
                    }));
                }
                catch (ObjectDisposedException)
                {
                    // ignore
                }
            });
        }

        public void OpenFilter_SearchForm()
        {
            Visible = false;

            var filter_SearchForm = new FilterSearchForm(DatabaseInstance.OrderingTable.ToArray());
            if (filter_SearchForm.ShowDialog() == DialogResult.OK)
            {
                bindingList.Clear();
                foreach (var model in filter_SearchForm.Result)
                    if (model is Ordering ordering)
                        bindingList.Add(ordering);
            }

            Visible = true;

            Activate();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            OpenOrderingForm();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!initializing)
            {
                DatabaseInstance.OrderingTable.Sync();
                if (e.ColumnIndex == 11)
                {
                    var check = (bool) dataGridView1[e.ColumnIndex, e.RowIndex].Value;
                    var orderingId = (string) dataGridView1[0, e.RowIndex].Value;
                    var ordering = DatabaseInstance.OrderingTable.Where(el => el.OrderingId == orderingId)
                        .FirstOrDefault();
                    if (ordering != null)
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
                            DatabaseInstance.UpdateUnion();
                        }
                }
            }
        }

        private void filterButton_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void OpenOrderingConfirmationForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseInstance.OrderingTable.Sync();
        }

        private void OpenOrderingConfirmationForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderingList();

            Activate();
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

                var form = new OrderingForm(employeeId);
                form.ShowDialog();

                Close();
            }
        }

        private void printing_Click(object sender, EventArgs e)
        {
            pdfFile = CreateDocument();

            var dialog = new PrintFilesDialog(pdfFile);
            dialog.ShowDialog();
        }

        private delegate void AsyncAction();
    }
}