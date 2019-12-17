using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MetroFramework.Forms;
using ObjectDatabase;

namespace SysDev2019
{
    public partial class FilterSearchForm : MetroForm
    {
        private readonly DataModel[] models;
        private DataModel model;

        public FilterSearchForm(DataModel[] model)
        {
            InitializeComponent();

            models = model;
        }

        public DataModel[] Result { get; private set; } = new DataModel[0];

        private void FieldSelect_SelectedIndexChanged(object sender, EventArgs ev)
        {
            ValueSelect.Items.Clear();

            if (ValueSelect.SelectedIndex != -1)
                return;

            var list = new List<string>();
            foreach (var dataModel in models.Select(m => m.Serialize()))
            {
                var val = dataModel.FirstOrDefault(e =>
                    e.Value.Name == FieldSelect.Items[FieldSelect.SelectedIndex].ToString());
                if (!string.IsNullOrEmpty(val.Key)) list.Add(val.Value.Value.ToString());
            }

            ValueSelect.Items.AddRange(list.Distinct().ToArray());
        }

        private void FilterButton_Click(object sender, EventArgs ev)
        {
            IEnumerable<DataModel> filter = null;
            var cond = ConditionsSelect.Items[ConditionsSelect.SelectedIndex].ToString();
            if (cond == "完全一致")
                filter = models.Where(e =>
                {
                    var datas = e.Serialize();
                    return datas.FirstOrDefault(d =>
                                   d.Value.Name == FieldSelect.Text && d.Value.Value.ToString() == ValueSelect.Text)
                               .Value != null;
                });
            else if (cond == "不一致")
                filter = models.Where(e =>
                {
                    var datas = e.Serialize();
                    return datas.FirstOrDefault(d =>
                                   d.Value.Name == FieldSelect.Text && d.Value.Value.ToString() != ValueSelect.Text)
                               .Value != null;
                });
            else if (cond == "部分一致")
                filter = models.Where(e =>
                {
                    var datas = e.Serialize();
                    return datas.FirstOrDefault(d =>
                                   d.Value.Name == FieldSelect.Text &&
                                   d.Value.Value.ToString().Contains(ValueSelect.Text))
                               .Value != null;
                });

            DialogResult = DialogResult.OK;
            Result = filter.ToArray();

            Close();
        }

        private void FilterSearchForm_Shown(object sender, EventArgs e)
        {
            if (models.Length == 0)
            {
                MessageBox.Show("検索するデータがありません。");
                Close();
                return;
            }

            model = models[0];

            foreach (var field in model.Serialize()) FieldSelect.Items.Add(field.Value.Name);

            FieldSelect.SelectedIndex = 0;

            ConditionsSelect.Items.Add("部分一致");
            ConditionsSelect.Items.Add("完全一致");
            ConditionsSelect.Items.Add("不一致");
            ConditionsSelect.SelectedIndex = 0;

            Activate();
        }
    }
}