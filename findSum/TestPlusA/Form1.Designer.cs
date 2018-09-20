namespace TestPlusA
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
            this.txtOld = new System.Windows.Forms.TextBox();
            this.btnFind = new System.Windows.Forms.Button();
            this.textBoxTarget = new System.Windows.Forms.TextBox();
            this.textBoxResult = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxRemove = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClearRemove = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtOld
            // 
            this.txtOld.AllowDrop = true;
            this.txtOld.Location = new System.Drawing.Point(12, 33);
            this.txtOld.Multiline = true;
            this.txtOld.Name = "txtOld";
            this.txtOld.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtOld.Size = new System.Drawing.Size(225, 34);
            this.txtOld.TabIndex = 45;
            this.txtOld.UseWaitCursor = true;
            this.txtOld.DragEnter += new System.Windows.Forms.DragEventHandler(this.txtOld_DragEnter_1);
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(129, 117);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(75, 23);
            this.btnFind.TabIndex = 46;
            this.btnFind.Text = "查找";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.UseWaitCursor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
            // 
            // textBoxTarget
            // 
            this.textBoxTarget.Location = new System.Drawing.Point(12, 117);
            this.textBoxTarget.Name = "textBoxTarget";
            this.textBoxTarget.Size = new System.Drawing.Size(100, 21);
            this.textBoxTarget.TabIndex = 47;
            this.textBoxTarget.UseWaitCursor = true;
            // 
            // textBoxResult
            // 
            this.textBoxResult.Location = new System.Drawing.Point(12, 179);
            this.textBoxResult.Multiline = true;
            this.textBoxResult.Name = "textBoxResult";
            this.textBoxResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxResult.Size = new System.Drawing.Size(234, 380);
            this.textBoxResult.TabIndex = 48;
            this.textBoxResult.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 49;
            this.label1.Text = "目标数字;";
            this.label1.UseWaitCursor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 50;
            this.label2.Text = "结果:";
            this.label2.UseWaitCursor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 51;
            this.label3.Text = "把Excel拖到这里";
            this.label3.UseWaitCursor = true;
            // 
            // textBoxRemove
            // 
            this.textBoxRemove.Location = new System.Drawing.Point(252, 179);
            this.textBoxRemove.Multiline = true;
            this.textBoxRemove.Name = "textBoxRemove";
            this.textBoxRemove.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxRemove.Size = new System.Drawing.Size(234, 380);
            this.textBoxRemove.TabIndex = 52;
            this.textBoxRemove.UseWaitCursor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 12);
            this.label4.TabIndex = 53;
            this.label4.Text = "已经移除的商家:";
            this.label4.UseWaitCursor = true;
            // 
            // btnClearRemove
            // 
            this.btnClearRemove.Location = new System.Drawing.Point(391, 150);
            this.btnClearRemove.Name = "btnClearRemove";
            this.btnClearRemove.Size = new System.Drawing.Size(75, 23);
            this.btnClearRemove.TabIndex = 54;
            this.btnClearRemove.Text = "清空";
            this.btnClearRemove.UseVisualStyleBackColor = true;
            this.btnClearRemove.UseWaitCursor = true;
            this.btnClearRemove.Click += new System.EventHandler(this.btnClearRemove_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 571);
            this.Controls.Add(this.btnClearRemove);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxRemove);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxResult);
            this.Controls.Add(this.textBoxTarget);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this.txtOld);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "超级智能查找工具";
            this.UseWaitCursor = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtOld;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.TextBox textBoxTarget;
        private System.Windows.Forms.TextBox textBoxResult;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxRemove;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClearRemove;
    }
}

