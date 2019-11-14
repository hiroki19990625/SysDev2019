﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysDev2019
{
    public partial class OpenOrderingConfirmationForm : Form
    {
        private string employeeId;
        private bool OpenOrdering;

        public bool CloseFlag = true;
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
                var orders = DatabaseInstance.OrderingTable.Where(e => e.EmployeeId == employeeId).ToArray();
               
                try
                {
                    Invoke(new AsyncAction(() =>
                    {
                        dataGridView1.DataSource = orders;
                        var cols = dataGridView1.Columns;
                        cols.RemoveAt(cols.Count - 1);
                        cols.RemoveAt(cols.Count - 1);
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
    }
}

