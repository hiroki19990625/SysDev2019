namespace SysDev2019
{
    partial class SalesStaffMenuForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.EmpName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button1.Location = new System.Drawing.Point(159, 259);
            this.button1.Margin = new System.Windows.Forms.Padding(150, 150, 150, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(564, 51);
            this.button1.TabIndex = 2;
            this.button1.Text = "注文入力画面";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("MS UI Gothic", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(159, 393);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 80, 3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(564, 51);
            this.button2.TabIndex = 3;
            this.button2.Text = "注文確認画面";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(209, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(200, 50, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(376, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "営業担当メニュー";
            // 
            // EmpName
            // 
            this.EmpName.AutoSize = true;
            this.EmpName.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.EmpName.Location = new System.Drawing.Point(638, 89);
            this.EmpName.Margin = new System.Windows.Forms.Padding(50, 0, 3, 0);
            this.EmpName.Name = "EmpName";
            this.EmpName.Size = new System.Drawing.Size(49, 20);
            this.EmpName.TabIndex = 1;
            this.EmpName.Text = "名前";
            // 
            // SalesStaffMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 553);
            this.Controls.Add(this.EmpName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SalesStaffMenuForm";
            this.Shown += new System.EventHandler(this.SalesStaffMenuForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label EmpName;
    }
}