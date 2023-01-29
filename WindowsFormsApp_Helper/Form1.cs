using ExpTreeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static ExpTreeLib.ExpTree;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp_Helper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) 
        {
        }

        private void TabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.expTree1.ExpTreeNodeSelected += new ExpTreeNodeSelectedEventHandler(this.ExpTree1_ExpTreeNodeSelected);

            expTree1.ExpandANode(@"C:\Users");
        }

        /// <summary>
        /// ツリービューのノードセレクト時処理
        /// </summary>
        /// <param name="SelPath"></param>
        /// <param name="CSI"></param>
        private void ExpTree1_ExpTreeNodeSelected(string SelPath, CShItem CSI)
        {
            listView1.View = View.Details;
            listView1.Clear();
            listView1.Columns.Add("名前");
            listView1.Columns.Add("種類");

            try
            {
                // フォルダをリストに追加
                DirectoryInfo dirInfo = new DirectoryInfo(SelPath);

                foreach (DirectoryInfo di in dirInfo.GetDirectories())
                {
                    ListViewItem item = new ListViewItem(di.Name);
                    item.SubItems.Add("フォルダ");
                    item.SubItems.Add("");
                    listView1.Items.Add(item);
                }

                // ファイルをリスト追加
                List<String> fileList = Directory.GetFiles(SelPath).ToList<String>();

                foreach (String file in fileList)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    ListViewItem item = new ListViewItem(fileInfo.Name);
                    item.SubItems.Add("ファイル");
                    listView1.Items.Add(item);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
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
                    }
                    else
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
