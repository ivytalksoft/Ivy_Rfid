using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RFID
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RFIDHelper.AutoOpenCard();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RFIDHelper.OpenCard(Convert.ToInt32(this.textBox1.Text));
            MessageBox.Show("打开成功");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RFIDHelper.CloseCardManager();
            MessageBox.Show("关闭成功");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<string> lis = RFIDHelper.ReadEPC();
            this.textBox2.Text = "";
            foreach (var str in lis)
            {
                this.textBox2.Text += str + Environment.NewLine;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            RFIDHelper.WriteEPC(textBox3.Text, RFIDResources.my_pwd);
            MessageBox.Show("写入完成");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RFIDHelper.ChangePwd(this.textBox2.Text.Trim(), this.txtOldPwd.Text, this.txtNewPwd.Text);
            MessageBox.Show("修改成功");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            RFIDHelper.ChangeAdminPwd(this.textBox2.Text.Trim(), this.txtOldPwd.Text, this.txtNewPwd.Text);
            MessageBox.Show("修改成功");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            this.txtData.Text = "";
            try
            {
                if (string.IsNullOrEmpty(txtNum.Text))
                {
                    foreach (string s in this.textBox2.Text.Split('\r'))
                    {
                        string st = s.Replace("\n", "");
                        if (string.IsNullOrEmpty(st))
                            continue;
                        string str = RFIDHelper.ReadData(st, RFIDResources.my_pwd);
                        str = RFIDHelper.ReadData(st, RFIDResources.default_pwd);
                        this.txtData.Text += str + Environment.NewLine;
                    }

                }
                else
                {
                    string str = RFIDHelper.ReadData(this.textBox2.Text.Trim(), RFIDResources.my_pwd, Convert.ToInt32(txtNum.Text));
                    this.txtData.Text = str;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCode.Text))
            {
                RFIDHelper.WriteData(this.textBox2.Text.Trim(), RFIDResources.my_pwd, this.txtWdata.Text.Trim());
            }
            else
            {
                RFIDHelper.WriteData(this.textBox2.Text.Trim(), RFIDResources.my_pwd, this.txtWdata.Text.Trim(), Convert.ToInt32(this.txtCode.Text));
            }
            MessageBox.Show("写入成功");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                string str = RFIDHelper.ReadData(this.txt_bq.Text.Trim(), RFIDResources.default_pwd);
                this.txtData.Text = str;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                string str = RFIDHelper.ReadDataOfTID(this.txt_bq.Text.Trim(), RFIDResources.default_pwd);
                this.txtData.Text = str;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
