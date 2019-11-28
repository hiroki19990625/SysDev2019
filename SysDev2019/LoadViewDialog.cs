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
    public partial class LoadViewDialog : Form
    {
        private Action _action;

        public LoadViewDialog()
        {
            InitializeComponent();

            progressBar1.MarqueeAnimationSpeed = 50;
        }

        public void SetCallback(Action action)
        {
            _action = action;
        }

        private void LoadViewDialog_Shown(object sender, EventArgs e)
        {
            _action();
        }
    }
}