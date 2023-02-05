using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApp_Helper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form1_Load(null,null);
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
            Console.WriteLine("おはよう");
            StreamReader sr = new StreamReader(@"C:\dev\SaveReport\data.txt");
            string text = sr.ReadToEnd();
            sr.Close();

        }

        #region 画面左側の機能
        /// <summary>
        /// [単一行加工]コピーボタンのクリックイベント
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
                    checkBox1.Checked = false;
                }
                else
                {
                    MessageBox.Show("入力値がありません。");
                    checkBox1.Checked = false;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// [複数行加工]コピーボタンのクリックイベント
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
                    MessageBox.Show("コピーしました。", "コピー完了");
                    textBox2.Clear();
                    checkBox2.Checked = false;
                }
                else 
                {
                    MessageBox.Show("入力値がありません。");
                    checkBox2.Checked = false;
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
                if (checkBox1.Checked)
                {
                    array[0] = string.Format("('{0}')", text[0]);
                }
                else
                {
                    array[0] = string.Format("'{0}'", text[0]);
                }
            }
            else
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (i == text.Length - 1)
                    {
                        if (checkBox2.Checked)
                        {
                            array[i] = string.Format("'{0}')", text[i]).ToString();
                        }
                        else 
                        {
                            array[i] = string.Format("'{0}'", text[i]).ToString();
                        }
                    }
                    else
                    {
                        if (checkBox2.Checked)
                        {
                            if (i == 0)
                            {
                                array[i] = string.Format("('{0}',", text[i]).ToString();
                            }
                            else
                            {
                                array[i] = string.Format("'{0}',", text[i]).ToString();
                            }
                        }
                        else
                        {
                            array[i] = string.Format("'{0}',", text[i]).ToString();
                        }
                    }
                }
            }
            return string.Join("\r\n", array);
        }

        /// <summary>
        /// [日付]コピーボタンのクリックイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
        private void Button3_Click(object sender, EventArgs e)
        {
            string dateTime = DateTime.Now.ToString("yyyy-MM-dd");
            MessageBox.Show(dateTime);
            Clipboard.SetText(dateTime);
        }

        /// <summary>
        /// [SQL_UPDATE]コピーボタンのクリックイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
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

        #endregion

        #region 画面右側の機能
        /// <summary>
        /// [ツリービュー]操作時のイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (treeView1.SelectedNode.Text == "ノード1")
            {
                textBox8.Text = string.Empty;
            }
            else 
            {
                textBox8.Text = string.Empty;
            }
        }
        #endregion

        /// <summary>
        /// [フォルダを開く]ボタンのクリックイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
        private void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                string inputPath = textBox7.Text;
                System.Diagnostics.Process.Start(inputPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error");
            }
        }

        /// <summary>
        /// [WEBサイトを開く]ボタンのクリックイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
        private void Button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox8.Text))
                {
                    string url = textBox8.Text;
                    System.Diagnostics.Process.Start($"{url}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Console.WriteLine("おやすみ");

            if (!Directory.Exists(@"C:\dev\SaveReport")) 
            {
                Directory.CreateDirectory(@"C:\dev\SaveReport\");
            }

            StreamWriter sw = new StreamWriter(@"C:\dev\SaveReport\data.txt");
            Note note = new Note
            {
                text1 = textBox7.Text,
                text2 = textBox8.Text
            };

            string json = JsonConvert.SerializeObject(note);
            sw.WriteLine(json);
            sw.Close();
            MessageBox.Show("保存しました。");
        }

        class Note 
        {
            public string text1 { get; set; }
            public string text2 { get; set; }
        }
    }
}
