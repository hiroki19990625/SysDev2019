﻿namespace SysDev2019
{
    partial class Filter_SearchForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.FieldSelect = new System.Windows.Forms.ComboBox();
            this.ConditionsSelect = new System.Windows.Forms.ComboBox();
            this.ValueSelect = new System.Windows.Forms.ComboBox();
            this.FilterButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.label2.Location = new System.Drawing.Point(349, 136);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 48);
            this.label2.TabIndex = 8;
            this.label2.Text = "フィールド";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.label1.Location = new System.Drawing.Point(349, 242);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 48);
            this.label1.TabIndex = 9;
            this.label1.Text = "条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.label3.Location = new System.Drawing.Point(349, 352);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 48);
            this.label3.TabIndex = 10;
            this.label3.Text = "値";
            // 
            // FieldSelect
            // 
            this.FieldSelect.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FieldSelect.FormattingEnabled = true;
            this.FieldSelect.Location = new System.Drawing.Point(588, 141);
            this.FieldSelect.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.FieldSelect.Name = "FieldSelect";
            this.FieldSelect.Size = new System.Drawing.Size(337, 45);
            this.FieldSelect.TabIndex = 11;
            // 
            // ConditionsSelect
            // 
            this.ConditionsSelect.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ConditionsSelect.FormattingEnabled = true;
            this.ConditionsSelect.Location = new System.Drawing.Point(588, 246);
            this.ConditionsSelect.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ConditionsSelect.Name = "ConditionsSelect";
            this.ConditionsSelect.Size = new System.Drawing.Size(337, 45);
            this.ConditionsSelect.TabIndex = 12;
            // 
            // ValueSelect
            // 
            this.ValueSelect.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ValueSelect.FormattingEnabled = true;
            this.ValueSelect.Location = new System.Drawing.Point(588, 357);
            this.ValueSelect.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ValueSelect.Name = "ValueSelect";
            this.ValueSelect.Size = new System.Drawing.Size(337, 45);
            this.ValueSelect.TabIndex = 13;
            // 
            // FilterButton
            // 
            this.FilterButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FilterButton.Location = new System.Drawing.Point(395, 507);
            this.FilterButton.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.FilterButton.Name = "FilterButton";
            this.FilterButton.Size = new System.Drawing.Size(431, 82);
            this.FilterButton.TabIndex = 14;
            this.FilterButton.Text = "絞り込み・検索";
            this.FilterButton.UseVisualStyleBackColor = true;
            this.FilterButton.Click += new System.EventHandler(this.FilterButton_Click);
            // 
            // Filter_SearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 720);
            this.Controls.Add(this.FilterButton);
            this.Controls.Add(this.ValueSelect);
            this.Controls.Add(this.ConditionsSelect);
            this.Controls.Add(this.FieldSelect);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Filter_SearchForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "絞り込み_検索画面";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox FieldSelect;
        private System.Windows.Forms.ComboBox ConditionsSelect;
        private System.Windows.Forms.ComboBox ValueSelect;
        private System.Windows.Forms.Button FilterButton;
    }
}