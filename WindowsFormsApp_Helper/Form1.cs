using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_Helper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string[] textList = textBox1.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);

                string addText = ChangeText(textList);
                MessageBox.Show(addText, "コピー完了");
                Clipboard.SetText(addText);
                this.textBox1.Clear();
            }

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text))
            {
                string[] textList = textBox2.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);

                string addText = ChangeText(textList);
                MessageBox.Show(addText, "コピー完了");
                Clipboard.SetText(addText);
                this.textBox2.Clear();
            }
        }
        private string ChangeText(string[] text)
        {
            string[] array = new string[text.Length];
            if (text.Length == 1)
            {
                array[0] = string.Format("'{0}'", text[0]);
            }
            else
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (i == text.Length - 1)
                    {
                        array[i] = string.Format("'{0}'", text[i]).ToString();
                    }
                    else
                    {
                        array[i] = string.Format("'{0}',", text[i]).ToString();
                    }
                }
            }
            return string.Join("\r\n", array);
        }
    }
}
