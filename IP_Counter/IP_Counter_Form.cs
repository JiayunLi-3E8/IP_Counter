using System;
using System.Windows.Forms;

namespace IP_Counter
{
    public partial class IP_Counter_Form : Form
    {
        public IP_Counter_Form()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == 8)
                e.Handled = false;
            if (e.KeyChar == '.' || e.KeyChar == 13)
                SendKeys.Send("{tab}");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte i;
                byte[] ip = new byte[4] { byte.Parse(textBox1.Text), byte.Parse(textBox2.Text), byte.Parse(textBox3.Text), byte.Parse(textBox4.Text) };
                byte[] zwym = new byte[4] { byte.Parse(textBox5.Text), byte.Parse(textBox6.Text), byte.Parse(textBox7.Text), byte.Parse(textBox8.Text) };
                byte[] fym = new byte[4] { (byte)(~zwym[0]), (byte)(~zwym[1]), (byte)(~zwym[2]), (byte)(~zwym[3]) };
                byte[] wlh = new byte[4] { (byte)(ip[0] & zwym[0]), (byte)(ip[1] & zwym[1]), (byte)(ip[2] & zwym[2]), (byte)(ip[3] & zwym[3]) };
                byte[] gbdz = new byte[4] { (byte)(ip[0] | (~zwym[0])), (byte)(ip[1] | (~zwym[1])), (byte)(ip[2] | (~zwym[2])), (byte)(ip[3] | (~zwym[3])) };
                uint ip32 = 0, zwym32 = 0, zjh, rnzjl;
                for (i = 0; i < 4; i++)
                {
                    ip32 <<= 8;
                    zwym32 <<= 8;
                    ip32 |= ip[i];
                    zwym32 |= zwym[i];
                }
                if((zwym32|(zwym32-1))!=0xffffffff)
                {
                    MessageBox.Show("请输入合法的子网掩码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                zjh = ip32 & (~zwym32);
                rnzjl = (~zwym32) - 1;
                textBox9.Text = wlh[0].ToString();
                textBox10.Text = wlh[1].ToString();
                textBox11.Text = wlh[2].ToString();
                textBox12.Text = wlh[3].ToString();
                textBox13.Text = gbdz[0].ToString();
                textBox14.Text = gbdz[1].ToString();
                textBox15.Text = gbdz[2].ToString();
                textBox16.Text = gbdz[3].ToString();
                textBox17.Text = zjh.ToString();
                textBox18.Text = rnzjl.ToString();
                textBox20.Text = fym[0].ToString();
                textBox21.Text = fym[1].ToString();
                textBox22.Text = fym[2].ToString();
                textBox23.Text = fym[3].ToString();
            }
            catch (FormatException)
            {
                MessageBox.Show("请输入完整！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (OverflowException)
            {
                MessageBox.Show("输入超出255！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("\t记得双击\n\t❤❤❤\n\t 么么哒", "❤❤❤爱你哦❤❤❤");
        }

        private void textBox1_8_Enter(object sender, EventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void textBox19_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == 8)
                e.Handled = false;
            if (e.KeyChar == 13)
                SendKeys.Send("{tab}");
        }

        private void IP_Mask_4_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked == true)
            {
                textBox19.ReadOnly = true;
                textBox19.TabStop = false;

                textBox5.ReadOnly = false;
                textBox5.TabStop = true;
                textBox6.ReadOnly = false;
                textBox6.TabStop = true;
                textBox7.ReadOnly = false;
                textBox7.TabStop = true;
                textBox8.ReadOnly = false;
                textBox8.TabStop = true;
            }
            else
            {
                textBox19.ReadOnly = false;
                textBox19.TabStop = true;

                textBox5.ReadOnly = true;
                textBox5.TabStop = false;
                textBox6.ReadOnly = true;
                textBox6.TabStop = false;
                textBox7.ReadOnly = true;
                textBox7.TabStop = false;
                textBox8.ReadOnly = true;
                textBox8.TabStop = false;
            }
        }

        private void textBox19_Leave(object sender, EventArgs e)
        {
            if (IP_Mask_1.Checked == true)
            {
                if (textBox19.Text == "")
                {
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox7.Text = "";
                    textBox8.Text = "";
                }
                else
                {
                    byte ip_mask_1 = byte.Parse(((TextBox)sender).Text);
                    if (ip_mask_1 > 32)
                    {
                        ip_mask_1 = 32;
                        textBox19.Text = "32";
                    }

                    uint ip_mask_32b = 0;
                    for (byte i = 0; i < ip_mask_1; i++)
                    {
                        ip_mask_32b >>= 1;
                        ip_mask_32b |= 0x80000000;
                    }

                    textBox5.Text = ((byte)(ip_mask_32b >> 24)).ToString();
                    textBox6.Text = ((byte)(ip_mask_32b >> 16)).ToString();
                    textBox7.Text = ((byte)(ip_mask_32b >> 8)).ToString();
                    textBox8.Text = ((byte)ip_mask_32b).ToString();
                }
            }
        }

        private void textBox5_8_Leave(object sender, EventArgs e)
        {
            if (IP_Mask_4.Checked == true && textBox5.Text != "" && textBox6.Text != "" && textBox7.Text != "" && textBox8.Text != "")
            {
                byte[] ip_mask_4 = new byte[4] { byte.Parse(textBox5.Text), byte.Parse(textBox6.Text), byte.Parse(textBox7.Text), byte.Parse(textBox8.Text) };
                uint ip_mask_32 = 0;
                for (byte i = 0; i < 4; i++)
                {
                    ip_mask_32 <<= 8;
                    ip_mask_32 |= ip_mask_4[i];
                }
                if ((ip_mask_32 | (ip_mask_32 - 1)) != 0xffffffff)
                {
                    MessageBox.Show("请输入合法的子网掩码！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                byte ip_mask_1 = 0;
                for (byte i = 0; i < 32; i++)
                {
                    if ((ip_mask_32 & 0x80000000) == 0x80000000)
                    {
                        ip_mask_1++;
                        ip_mask_32 <<= 1;
                    }
                    else
                    {
                        break;
                    }
                }

                textBox19.Text = ip_mask_1.ToString();
            }
        }

        private void IP_JSQ_Form_Load(object sender, EventArgs e)
        {
            IP_Mask_4.Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Control c in this.Controls)//遍历所有控件
            {
                if (c is TextBox)//判断是否是textBox控件，是则清空
                {
                    c.Text = "";
                }
            }
        }
    }
}
