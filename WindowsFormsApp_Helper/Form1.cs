using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WindowsFormsApp_Helper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// クリックイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    string[] textList = textBox1.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);
                    string addText = ChangeText(textList);
                    Clipboard.SetText(addText);
                    MessageBox.Show(addText, "コピー完了");
                    textBox1.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// クリックイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    string[] textList = textBox2.Text.Split(new[] { "\r\n" }, StringSplitOptions.None);
                    string addText = ChangeText(textList);
                    Clipboard.SetText(addText);
                    MessageBox.Show(addText, "コピー完了");
                    textBox2.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 文字加工メソッド
        /// </summary>
        /// <param name="text">入力内容</param>
        /// <returns>文字列(入力内容をシングルコートで囲む)</returns>
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

        private void Button3_Click(object sender, EventArgs e)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            MessageBox.Show(dateTime);
            Clipboard.SetText(dateTime);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string tableName = string.IsNullOrEmpty(textBox3.Text) ? string.Empty : textBox3.Text;
            string updateItem1 = string.IsNullOrEmpty(textBox4.Text) ? string.Empty : string.Format("{0} = ,", textBox4.Text);
            string updateItem2 = string.IsNullOrEmpty(textBox5.Text) ? string.Empty : string.Format("{0} = ", textBox5.Text);
            string where = string.IsNullOrEmpty(textBox6.Text) ? string.Empty : string.Format("{0} = ", textBox6.Text);

            List<string> sql = new List<string>
            {
                "BEGIN TRANSACTION",
                "UPDATE "+tableName,
                "SET",
                updateItem1,
                updateItem2,
                "WHERE",
                where,
                "--ROLLBACK",
                "--COMMIT"
            };
            string sqlstr = string.Join("\r\n", sql);
            Clipboard.SetText(sqlstr);
            MessageBox.Show(sqlstr,"コピー完了");
        }

      
    }
}
