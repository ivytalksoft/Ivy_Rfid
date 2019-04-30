namespace RFID
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
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.txtOldPwd = new System.Windows.Forms.TextBox();
            this.txtNewPwd = new System.Windows.Forms.TextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.TextBox();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.button9 = new System.Windows.Forms.Button();
            this.txtWdata = new System.Windows.Forms.TextBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txt_bq = new System.Windows.Forms.TextBox();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(37, 35);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(83, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "自动打开";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(37, 73);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(83, 32);
            this.button2.TabIndex = 1;
            this.button2.Text = "打开";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(126, 79);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 25);
            this.textBox1.TabIndex = 2;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(37, 111);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(83, 32);
            this.button3.TabIndex = 3;
            this.button3.Text = "关闭";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(383, 24);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(90, 32);
            this.button4.TabIndex = 4;
            this.button4.Text = "获取标签";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(493, 24);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(236, 61);
            this.textBox2.TabIndex = 5;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(383, 79);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(90, 32);
            this.button5.TabIndex = 6;
            this.button5.Text = "写入标签";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(493, 102);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(236, 32);
            this.textBox3.TabIndex = 7;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(161, 252);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(114, 32);
            this.button6.TabIndex = 8;
            this.button6.Text = "修改销毁密码";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(37, 252);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(114, 32);
            this.button7.TabIndex = 9;
            this.button7.Text = "修改密码";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // txtOldPwd
            // 
            this.txtOldPwd.Location = new System.Drawing.Point(37, 211);
            this.txtOldPwd.Name = "txtOldPwd";
            this.txtOldPwd.Size = new System.Drawing.Size(114, 25);
            this.txtOldPwd.TabIndex = 10;
            // 
            // txtNewPwd
            // 
            this.txtNewPwd.Location = new System.Drawing.Point(161, 211);
            this.txtNewPwd.Name = "txtNewPwd";
            this.txtNewPwd.Size = new System.Drawing.Size(114, 25);
            this.txtNewPwd.TabIndex = 11;
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(359, 190);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(56, 32);
            this.button8.TabIndex = 12;
            this.button8.Text = "读取数据";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(359, 228);
            this.txtData.Multiline = true;
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(442, 261);
            this.txtData.TabIndex = 13;
            // 
            // txtNum
            // 
            this.txtNum.Location = new System.Drawing.Point(665, 190);
            this.txtNum.Multiline = true;
            this.txtNum.Name = "txtNum";
            this.txtNum.Size = new System.Drawing.Size(136, 32);
            this.txtNum.TabIndex = 14;
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(37, 338);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(114, 32);
            this.button9.TabIndex = 15;
            this.button9.Text = "写入数据";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // txtWdata
            // 
            this.txtWdata.Location = new System.Drawing.Point(37, 376);
            this.txtWdata.Multiline = true;
            this.txtWdata.Name = "txtWdata";
            this.txtWdata.Size = new System.Drawing.Size(238, 86);
            this.txtWdata.TabIndex = 16;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(161, 344);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(114, 25);
            this.txtCode.TabIndex = 17;
            // 
            // txt_bq
            // 
            this.txt_bq.Location = new System.Drawing.Point(359, 152);
            this.txt_bq.Multiline = true;
            this.txt_bq.Name = "txt_bq";
            this.txt_bq.Size = new System.Drawing.Size(270, 32);
            this.txt_bq.TabIndex = 18;
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(421, 190);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(52, 32);
            this.button10.TabIndex = 19;
            this.button10.Text = "读取数据2";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(479, 190);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(43, 32);
            this.button11.TabIndex = 20;
            this.button11.Text = "TID";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 501);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.txt_bq);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtWdata);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.txtNum);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.txtNewPwd);
            this.Controls.Add(this.txtOldPwd);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.TextBox txtOldPwd;
        private System.Windows.Forms.TextBox txtNewPwd;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.TextBox txtWdata;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txt_bq;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
    }
}

