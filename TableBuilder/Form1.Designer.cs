namespace TableBuilder
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.FourForms = new System.Windows.Forms.CheckBox();
            this.CaseInfo = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dbConnection = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(142, 29);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "打开表格";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FourForms
            // 
            this.FourForms.AutoSize = true;
            this.FourForms.Location = new System.Drawing.Point(33, 24);
            this.FourForms.Name = "FourForms";
            this.FourForms.Size = new System.Drawing.Size(78, 16);
            this.FourForms.TabIndex = 1;
            this.FourForms.Tag = "";
            this.FourForms.Text = "FourForms";
            this.FourForms.UseVisualStyleBackColor = true;
            this.FourForms.CheckedChanged += new System.EventHandler(this.FourForms_CheckedChanged);
            // 
            // CaseInfo
            // 
            this.CaseInfo.AutoSize = true;
            this.CaseInfo.Location = new System.Drawing.Point(33, 57);
            this.CaseInfo.Name = "CaseInfo";
            this.CaseInfo.Size = new System.Drawing.Size(72, 16);
            this.CaseInfo.TabIndex = 2;
            this.CaseInfo.Text = "CaseInfo";
            this.CaseInfo.UseVisualStyleBackColor = true;
            this.CaseInfo.CheckedChanged += new System.EventHandler(this.CaseInfo_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(33, 215);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(301, 221);
            this.textBox1.TabIndex = 3;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(29, 193);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "生成结果";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // dbConnection
            // 
            this.dbConnection.Location = new System.Drawing.Point(33, 128);
            this.dbConnection.Name = "dbConnection";
            this.dbConnection.Size = new System.Drawing.Size(97, 33);
            this.dbConnection.TabIndex = 5;
            this.dbConnection.Text = "测试数据库连接";
            this.dbConnection.UseVisualStyleBackColor = true;
            this.dbConnection.Click += new System.EventHandler(this.dbConnection_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.dbConnection);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CaseInfo);
            this.Controls.Add(this.FourForms);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Tag = "1111";
            this.Text = "TableBuilder";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox FourForms;
        private System.Windows.Forms.CheckBox CaseInfo;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button dbConnection;
    }
}

