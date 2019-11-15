﻿namespace SysDev2019
{
    partial class OpenOrderingConfirmationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.backButton = new System.Windows.Forms.Button();
            this.filterButton = new System.Windows.Forms.Button();
            this.printing = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(413, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(412, 64);
            this.label1.TabIndex = 2;
            this.label1.Text = "発注確認画面";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(55, 134);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 82;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1190, 453);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            // 
            // backButton
            // 
            this.backButton.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.backButton.Location = new System.Drawing.Point(224, 611);
            this.backButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(346, 90);
            this.backButton.TabIndex = 8;
            this.backButton.Text = "発注画面へ";
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // filterButton
            // 
            this.filterButton.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.filterButton.Location = new System.Drawing.Point(687, 611);
            this.filterButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.filterButton.Name = "filterButton";
            this.filterButton.Size = new System.Drawing.Size(346, 90);
            this.filterButton.TabIndex = 9;
            this.filterButton.Text = "絞り込み";
            this.filterButton.UseVisualStyleBackColor = true;
            this.filterButton.Click += new System.EventHandler(this.filterButton_Click);
            // 
            // printing
            // 
            this.printing.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.printing.Location = new System.Drawing.Point(899, 35);
            this.printing.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.printing.Name = "printing";
            this.printing.Size = new System.Drawing.Size(346, 90);
            this.printing.TabIndex = 10;
            this.printing.Text = "発注表の印刷";
            this.printing.UseVisualStyleBackColor = true;
            // 
            // OpenOrderingConfirmationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 720);
            this.Controls.Add(this.printing);
            this.Controls.Add(this.filterButton);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OpenOrderingConfirmationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenOrderingConfirmationForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OpenOrderingConfirmationForm_FormClosed);
            this.Load += new System.EventHandler(this.OpenOrderingConfirmationForm_Load);
            this.Shown += new System.EventHandler(this.OpenOrderingConfirmationForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button filterButton;
        private System.Windows.Forms.Button printing;
    }
}