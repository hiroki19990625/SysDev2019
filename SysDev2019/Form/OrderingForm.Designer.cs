﻿namespace SysDev2019
{
    partial class OrderingForm
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
            this.Manufacturer = new System.Windows.Forms.ComboBox();
            this.product = new System.Windows.Forms.ComboBox();
            this.count = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.orderingButton = new System.Windows.Forms.Button();
            this.orderingConfirmButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.count)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(329, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(320, 50, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "発注画面";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Manufacturer
            // 
            this.Manufacturer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.Manufacturer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.Manufacturer.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Manufacturer.FormattingEnabled = true;
            this.Manufacturer.Location = new System.Drawing.Point(464, 182);
            this.Manufacturer.Margin = new System.Windows.Forms.Padding(3, 3, 200, 3);
            this.Manufacturer.Name = "Manufacturer";
            this.Manufacturer.Size = new System.Drawing.Size(209, 31);
            this.Manufacturer.TabIndex = 2;
            this.Manufacturer.SelectedIndexChanged += new System.EventHandler(this.Manufacturer_SelectedIndexChanged);
            this.Manufacturer.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Manufacturer_KeyDown);
            this.Manufacturer.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Manufacturer_KeyPress);
            // 
            // product
            // 
            this.product.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.product.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.product.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.product.FormattingEnabled = true;
            this.product.Location = new System.Drawing.Point(464, 253);
            this.product.Margin = new System.Windows.Forms.Padding(3, 3, 200, 3);
            this.product.Name = "product";
            this.product.Size = new System.Drawing.Size(209, 31);
            this.product.TabIndex = 4;
            this.product.KeyDown += new System.Windows.Forms.KeyEventHandler(this.product_KeyDown);
            this.product.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.product_KeyPress);
            // 
            // count
            // 
            this.count.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.count.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.count.Location = new System.Drawing.Point(464, 331);
            this.count.Margin = new System.Windows.Forms.Padding(3, 3, 200, 3);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(209, 30);
            this.count.TabIndex = 6;
            this.count.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.count.KeyDown += new System.Windows.Forms.KeyEventHandler(this.count_KeyDown);
            this.count.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.count_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.label2.Location = new System.Drawing.Point(209, 183);
            this.label2.Margin = new System.Windows.Forms.Padding(200, 0, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "メーカー";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.label3.Location = new System.Drawing.Point(209, 254);
            this.label3.Margin = new System.Windows.Forms.Padding(200, 0, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 30);
            this.label3.TabIndex = 3;
            this.label3.Text = "商品";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.label4.Location = new System.Drawing.Point(209, 331);
            this.label4.Margin = new System.Windows.Forms.Padding(200, 0, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 30);
            this.label4.TabIndex = 5;
            this.label4.Text = "個数";
            // 
            // orderingButton
            // 
            this.orderingButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.orderingButton.Location = new System.Drawing.Point(109, 490);
            this.orderingButton.Margin = new System.Windows.Forms.Padding(100, 3, 3, 3);
            this.orderingButton.Name = "orderingButton";
            this.orderingButton.Size = new System.Drawing.Size(303, 51);
            this.orderingButton.TabIndex = 7;
            this.orderingButton.Text = "発注";
            this.orderingButton.UseVisualStyleBackColor = true;
            this.orderingButton.Click += new System.EventHandler(this.orderingButton_Click);
            // 
            // orderingConfirmButton
            // 
            this.orderingConfirmButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.orderingConfirmButton.Location = new System.Drawing.Point(470, 490);
            this.orderingConfirmButton.Margin = new System.Windows.Forms.Padding(3, 3, 100, 3);
            this.orderingConfirmButton.Name = "orderingConfirmButton";
            this.orderingConfirmButton.Size = new System.Drawing.Size(303, 51);
            this.orderingConfirmButton.TabIndex = 8;
            this.orderingConfirmButton.Text = "発注して確認";
            this.orderingConfirmButton.UseVisualStyleBackColor = true;
            this.orderingConfirmButton.Click += new System.EventHandler(this.orderingConfirmButton_Click);
            // 
            // OrderingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.orderingConfirmButton);
            this.Controls.Add(this.orderingButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.count);
            this.Controls.Add(this.product);
            this.Controls.Add(this.Manufacturer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OrderingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Shown += new System.EventHandler(this.OpenOrderingFrom_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox Manufacturer;
        private System.Windows.Forms.ComboBox product;
        private System.Windows.Forms.NumericUpDown count;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button orderingButton;
        private System.Windows.Forms.Button orderingConfirmButton;
    }
}