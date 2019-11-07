namespace SysDev2019
{
    partial class LogisticsManagerMenuForm
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
            this.EmpName = new System.Windows.Forms.Label();
            this.stockListButton = new System.Windows.Forms.Button();
            this.orderingButton = new System.Windows.Forms.Button();
            this.orderingConfirmationButton = new System.Windows.Forms.Button();
            this.orderConfirmationButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 30F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(196, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "物流担当メニュー";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // EmpName
            // 
            this.EmpName.AutoSize = true;
            this.EmpName.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.EmpName.Location = new System.Drawing.Point(608, 39);
            this.EmpName.Name = "EmpName";
            this.EmpName.Size = new System.Drawing.Size(49, 20);
            this.EmpName.TabIndex = 1;
            this.EmpName.Text = "名前";
            // 
            // stockListButton
            // 
            this.stockListButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold);
            this.stockListButton.Location = new System.Drawing.Point(52, 146);
            this.stockListButton.Name = "stockListButton";
            this.stockListButton.Size = new System.Drawing.Size(413, 51);
            this.stockListButton.TabIndex = 2;
            this.stockListButton.Text = "在庫一覧・発注点設定画面";
            this.stockListButton.UseVisualStyleBackColor = true;
            this.stockListButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // orderingButton
            // 
            this.orderingButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold);
            this.orderingButton.Location = new System.Drawing.Point(495, 146);
            this.orderingButton.Name = "orderingButton";
            this.orderingButton.Size = new System.Drawing.Size(240, 51);
            this.orderingButton.TabIndex = 3;
            this.orderingButton.Text = "発注画面";
            this.orderingButton.UseVisualStyleBackColor = true;
            this.orderingButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // orderingConfirmationButton
            // 
            this.orderingConfirmationButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold);
            this.orderingConfirmationButton.Location = new System.Drawing.Point(89, 256);
            this.orderingConfirmationButton.Name = "orderingConfirmationButton";
            this.orderingConfirmationButton.Size = new System.Drawing.Size(296, 57);
            this.orderingConfirmationButton.TabIndex = 4;
            this.orderingConfirmationButton.Text = "発注確認画面";
            this.orderingConfirmationButton.UseVisualStyleBackColor = true;
            this.orderingConfirmationButton.Click += new System.EventHandler(this.button3_Click);
            // 
            // orderConfirmationButton
            // 
            this.orderConfirmationButton.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold);
            this.orderConfirmationButton.Location = new System.Drawing.Point(474, 256);
            this.orderConfirmationButton.Name = "orderConfirmationButton";
            this.orderConfirmationButton.Size = new System.Drawing.Size(273, 57);
            this.orderConfirmationButton.TabIndex = 5;
            this.orderConfirmationButton.Text = "受注確認画面";
            this.orderConfirmationButton.UseVisualStyleBackColor = true;
            this.orderConfirmationButton.Click += new System.EventHandler(this.button4_Click);
            // 
            // LogisticsManagerMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.orderConfirmationButton);
            this.Controls.Add(this.orderingConfirmationButton);
            this.Controls.Add(this.orderingButton);
            this.Controls.Add(this.stockListButton);
            this.Controls.Add(this.EmpName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LogisticsManagerMenuForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogisticsManagerMenuForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label EmpName;
        private System.Windows.Forms.Button stockListButton;
        private System.Windows.Forms.Button orderingButton;
        private System.Windows.Forms.Button orderingConfirmationButton;
        private System.Windows.Forms.Button orderConfirmationButton;
    }
}