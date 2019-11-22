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

            if (model.Length == 0)
                return;
            this.models = model;
            this.model = model[0];
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

        private void FilterButton_Click(object sender, EventArgs ev)
        {
            var filter = models.Where(e =>
            {
                var datas = e.Serialize();
                return datas.FirstOrDefault(d =>
                               d.Value.Name == FieldSelect.Text && d.Value.Value.ToString() == ValueSelect.Text)
                           .Value != null;
            });

            Result = filter.ToArray();

            Close();
        }

        private void FieldSelect_SelectedIndexChanged(object sender, EventArgs ev)
        {
            ValueSelect.Items.Clear();

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
    }
}