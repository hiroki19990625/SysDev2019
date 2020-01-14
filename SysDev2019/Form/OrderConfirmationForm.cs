using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;
using SysDev2019.DataModels;

namespace SysDev2019
{
    public partial class OrderConfirmationForm : MetroForm
    {
        private readonly BindingList<Order> _bindingList = new BindingList<Order>();
        private string _employeeId;
        public bool CloseFlag = true;

        public OrderConfirmationForm(string employeeId)
        {
            InitializeComponent();

            _employeeId = employeeId;
        }

        public OrderConfirmationForm()
        {
            InitializeComponent();
        }

        public void InitializeOrderList()
        {
            Task.Run(() =>
            {
                var orders = DatabaseInstance.OrderTable.Where(e => !e.ShipmentCompleted).ToArray();

                try
                {
                    Invoke(new AsyncAction(() =>
                    {
                        dataGridView1.DataSource = _bindingList;

                        dataGridView1.SuspendLayout();
                        foreach (var order in orders) _bindingList.Add(order);

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
                        dataGridView1.Columns[10].HeaderText = "受注完了";
                        dataGridView1.Columns[11].HeaderText = "注文キャンセル";
                        dataGridView1.Columns[12].HeaderText = "出荷完了";

                        dataGridView1.Columns[1].Visible = false;
                        dataGridView1.Columns[4].Visible = false;
                        dataGridView1.Columns[13].Visible = false;
                        dataGridView1.Columns[14].Visible = false;

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
                        dataGridView1.Columns[11].ReadOnly = true;

                        dataGridView1.ResumeLayout();
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

            var filter_SearchForm = new FilterSearchForm(DatabaseInstance.OrderTable.ToArray());
            if (filter_SearchForm.ShowDialog() == DialogResult.OK)
            {
                _bindingList.Clear();
                foreach (var model in filter_SearchForm.Result)
                    if (model is Order order)
                        _bindingList.Add(order);
            }

            Visible = true;

            Activate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFilter_SearchForm();
        }

        private void OpenOrder_Confirmation_Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            DatabaseInstance.OrderTable.Sync();
        }

        private void OpenOrderConfirmationForm_Shown(object sender, EventArgs e)
        {
            InitializeOrderList();

            Activate();
        }

        private delegate void AsyncAction();

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                e.Handled = true;
                e.PaintBackground(e.ClipBounds, false);
                string text = dataGridView1.Columns[e.ColumnIndex].HeaderText;
                Font font = dataGridView1.ColumnHeadersDefaultCellStyle.Font;
                Brush foreBrush = new SolidBrush(
                    dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor);
                StringFormat sf = new StringFormat(StringFormatFlags.DirectionVertical);
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                e.Graphics.DrawString(text, font, foreBrush, e.CellBounds, sf);
            }
        }
    }
}