using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ObjectDatabase;
using SysDev2019.DataModels;

namespace SysDev2019
{
    public partial class FilterSearchForm : Form
    {
        private DataModel[] models;
        private DataModel model;

        public DataModel[] Result { get; private set; } = new DataModel[0];

        public FilterSearchForm(DataModel[] model)
        {
            InitializeComponent();

            this.models = model;      
        }

        private void FilterButton_Click(object sender, EventArgs ev)
        {
            IEnumerable<DataModel> filter = null;
            string cond = ConditionsSelect.Items[ConditionsSelect.SelectedIndex].ToString();
            if (cond == "完全一致")
            {
                filter = models.Where(e =>
                {
                    var datas = e.Serialize();
                    return datas.FirstOrDefault(d =>
                                   d.Value.Name == FieldSelect.Text && d.Value.Value.ToString() == ValueSelect.Text)
                               .Value != null;
                });
            }
            else if (cond == "不一致")
            {
                filter = models.Where(e =>
                {
                    var datas = e.Serialize();
                    return datas.FirstOrDefault(d =>
                                   d.Value.Name == FieldSelect.Text && d.Value.Value.ToString() != ValueSelect.Text)
                               .Value != null;
                });
            }
            else if (cond == "部分一致")
            {
                filter = models.Where(e =>
                {
                    var datas = e.Serialize();
                    return datas.FirstOrDefault(d =>
                                   d.Value.Name == FieldSelect.Text &&
                                   d.Value.Value.ToString().Contains(ValueSelect.Text))
                               .Value != null;
                });
            }

            DialogResult = DialogResult.OK;
            Result = filter.ToArray();

            Close();
        }

        private void FieldSelect_SelectedIndexChanged(object sender, EventArgs ev)
        {
            ValueSelect.Items.Clear();

            if (ValueSelect.SelectedIndex != -1)
                return;

            List<string> list = new List<string>();
            foreach (var dataModel in models.Select(m => m.Serialize()))
            {
                var val = dataModel.FirstOrDefault(e =>
                    e.Value.Name == FieldSelect.Items[FieldSelect.SelectedIndex].ToString());
                if (!string.IsNullOrEmpty(val.Key))
                {
                    list.Add(val.Value.Value.ToString());
                }
            }

            ValueSelect.Items.AddRange(list.Distinct().ToArray());
        }

        private void FilterSearchForm_Shown(object sender, EventArgs e)
        {
            if (models.Length == 0)
            {
                MessageBox.Show("検索するデータがありません。");
                Close();
                return;
            }

            this.model = models[0];

            foreach (var field in this.model.Serialize())
            {
                FieldSelect.Items.Add(field.Value.Name);
            }

            FieldSelect.SelectedIndex = 0;

            ConditionsSelect.Items.Add("部分一致");
            ConditionsSelect.Items.Add("完全一致");
            ConditionsSelect.Items.Add("不一致");
            ConditionsSelect.SelectedIndex = 0;

        }
    }
}