﻿namespace SysDev2019
{
    partial class OrderEntryForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.product = new System.Windows.Forms.ComboBox();
            this.count = new System.Windows.Forms.NumericUpDown();
            this.orderButton = new System.Windows.Forms.Button();
            this.orderConfirmButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.count)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(244, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(328, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "注文入力画面";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(248, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "商品";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(248, 230);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 30);
            this.label3.TabIndex = 2;
            this.label3.Text = "個数";
            // 
            // product
            // 
            this.product.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.product.FormattingEnabled = true;
            this.product.Location = new System.Drawing.Point(347, 150);
            this.product.Name = "product";
            this.product.Size = new System.Drawing.Size(209, 31);
            this.product.TabIndex = 3;
            this.product.SelectedIndexChanged += new System.EventHandler(this.product_SelectedIndexChanged);
            // 
            // count
            // 
            this.count.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.count.Location = new System.Drawing.Point(347, 230);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(209, 30);
            this.count.TabIndex = 4;
            this.count.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // orderButton
            // 
            this.orderButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.orderButton.Location = new System.Drawing.Point(66, 343);
            this.orderButton.Name = "orderButton";
            this.orderButton.Size = new System.Drawing.Size(303, 51);
            this.orderButton.TabIndex = 5;
            this.orderButton.Text = "注文";
            this.orderButton.UseVisualStyleBackColor = true;
            this.orderButton.Click += new System.EventHandler(this.orderEntryButton_Click);
            // 
            // orderConfirmButton
            // 
            this.orderConfirmButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.orderConfirmButton.Location = new System.Drawing.Point(454, 343);
            this.orderConfirmButton.Name = "orderConfirmButton";
            this.orderConfirmButton.Size = new System.Drawing.Size(303, 51);
            this.orderConfirmButton.TabIndex = 6;
            this.orderConfirmButton.Text = "注文して確認";
            this.orderConfirmButton.UseVisualStyleBackColor = true;
            this.orderConfirmButton.Click += new System.EventHandler(this.orderConfirmButton_Click);
            // 
            // OrderEntryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.orderConfirmButton);
            this.Controls.Add(this.orderButton);
            this.Controls.Add(this.count);
            this.Controls.Add(this.product);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OrderEntryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OrderEntryForm";
            this.Shown += new System.EventHandler(this.OrderEntryForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.count)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox product;
        private System.Windows.Forms.NumericUpDown count;
        private System.Windows.Forms.Button orderButton;
        private System.Windows.Forms.Button orderConfirmButton;
    }
}