namespace WindowsFormsApplication1
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ChooseFile = new System.Windows.Forms.Button();
            this.deal = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.choiceColumn = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ChooseFile
            // 
            this.ChooseFile.Location = new System.Drawing.Point(25, 33);
            this.ChooseFile.Name = "ChooseFile";
            this.ChooseFile.Size = new System.Drawing.Size(100, 40);
            this.ChooseFile.TabIndex = 0;
            this.ChooseFile.Text = "选择文件";
            this.ChooseFile.UseVisualStyleBackColor = true;
            this.ChooseFile.Click += new System.EventHandler(this.button1_Click);
            // 
            // deal
            // 
            this.deal.Location = new System.Drawing.Point(25, 105);
            this.deal.Name = "deal";
            this.deal.Size = new System.Drawing.Size(100, 40);
            this.deal.TabIndex = 1;
            this.deal.Text = "处理";
            this.deal.UseVisualStyleBackColor = true;
            this.deal.Click += new System.EventHandler(this.deal_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(25, 179);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(100, 40);
            this.save.TabIndex = 2;
            this.save.Text = "保存";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // choiceColumn
            // 
            this.choiceColumn.Location = new System.Drawing.Point(318, 41);
            this.choiceColumn.Name = "choiceColumn";
            this.choiceColumn.Size = new System.Drawing.Size(98, 21);
            this.choiceColumn.TabIndex = 3;
            this.choiceColumn.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(242, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "第几列数据:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 282);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.choiceColumn);
            this.Controls.Add(this.save);
            this.Controls.Add(this.deal);
            this.Controls.Add(this.ChooseFile);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ChooseFile;
        private System.Windows.Forms.Button deal;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.TextBox choiceColumn;
        private System.Windows.Forms.Label label1;
    }
}

