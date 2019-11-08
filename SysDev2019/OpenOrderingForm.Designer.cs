namespace SysDev2019
{
    partial class OpenOrderingForm
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
            this.label1.Location = new System.Drawing.Point(278, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(226, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "発注画面";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Manufacturer
            // 
            this.Manufacturer.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Manufacturer.FormattingEnabled = true;
            this.Manufacturer.Location = new System.Drawing.Point(325, 138);
            this.Manufacturer.Name = "Manufacturer";
            this.Manufacturer.Size = new System.Drawing.Size(209, 31);
            this.Manufacturer.TabIndex = 4;
            // 
            // product
            // 
            this.product.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.product.FormattingEnabled = true;
            this.product.Location = new System.Drawing.Point(325, 209);
            this.product.Name = "product";
            this.product.Size = new System.Drawing.Size(209, 31);
            this.product.TabIndex = 5;
            // 
            // count
            // 
            this.count.Font = new System.Drawing.Font("MS UI Gothic", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.count.Location = new System.Drawing.Point(325, 287);
            this.count.Name = "count";
            this.count.Size = new System.Drawing.Size(209, 30);
            this.count.TabIndex = 6;
            this.count.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.label2.Location = new System.Drawing.Point(204, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 30);
            this.label2.TabIndex = 7;
            this.label2.Text = "メーカー";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.label3.Location = new System.Drawing.Point(216, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 30);
            this.label3.TabIndex = 8;
            this.label3.Text = "商品";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MS UI Gothic", 18F);
            this.label4.Location = new System.Drawing.Point(216, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 30);
            this.label4.TabIndex = 9;
            this.label4.Text = "個数";
            // 
            // orderingButton
            // 
            this.orderingButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.orderingButton.Location = new System.Drawing.Point(128, 377);
            this.orderingButton.Name = "orderingButton";
            this.orderingButton.Size = new System.Drawing.Size(221, 51);
            this.orderingButton.TabIndex = 10;
            this.orderingButton.Text = "発注";
            this.orderingButton.UseVisualStyleBackColor = true;
            this.orderingButton.Click += new System.EventHandler(this.orderingButton_Click);
            // 
            // orderingConfirmButton
            // 
            this.orderingConfirmButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.orderingConfirmButton.Location = new System.Drawing.Point(458, 377);
            this.orderingConfirmButton.Name = "orderingConfirmButton";
            this.orderingConfirmButton.Size = new System.Drawing.Size(250, 51);
            this.orderingConfirmButton.TabIndex = 11;
            this.orderingConfirmButton.Text = "発注して確認";
            this.orderingConfirmButton.UseVisualStyleBackColor = true;
            this.orderingConfirmButton.Click += new System.EventHandler(this.orderingConfirmButton_Click);
            // 
            // OpenOrderingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.orderingConfirmButton);
            this.Controls.Add(this.orderingButton);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.count);
            this.Controls.Add(this.product);
            this.Controls.Add(this.Manufacturer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OpenOrderingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenOrderingForm";
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