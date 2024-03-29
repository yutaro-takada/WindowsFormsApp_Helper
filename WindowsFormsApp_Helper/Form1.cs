﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp_Helper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初回表示時のイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
        /// <remarks>Form1初回表示時のイベント</remarks>
        private void Form1_Shown(object sender , EventArgs e) 
        {
            TreeView1_BeforeSelected();
        }

        #region 画面左側の機能

        /// <summary>
        /// [複数行加工]コピーボタンのクリックイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
        private void multileLineCopyButton_Click(object sender, EventArgs e)
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
                    surroundMultipleLineCheckBox.Checked = false;
                }
                else 
                {
                    MessageBox.Show("入力値がありません。");
                    surroundMultipleLineCheckBox.Checked = false;
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
        /// <returns>文字列</returns>
        private string ChangeText(string[] text)
        {
            string[] array = new string[text.Length];
            /*単一行*/
            if (text.Length == 1)
            {
                if (surroundMultipleLineCheckBox.Checked)
                {
                    array[0] = string.Format("('{0}')", text[0]);
                }
                else
                {
                    array[0] = string.Format("'{0}'", text[0]);
                }
            }
            /*複数行*/
            else
            {
                for (int i = 0; i < text.Length; i++)
                {
                    if (i == text.Length - 1)
                    {
                        if (surroundMultipleLineCheckBox.Checked)
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
                        if (surroundMultipleLineCheckBox.Checked)
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
            /*【シングルクォーテーション】の除去*/
            if (exclusionSingleQuoteCheckBox.Checked)
            {
                string[] exSingleQuote = new string[array.Length];

                for (int i = 0; i < array.Length; i++)
                {
                    exSingleQuote[i] = array[i].Replace("'", "");
                }
                return string.Join("\r\n", exSingleQuote);
            }
            else
            {
                return string.Join("\r\n", array);
            }
        }

        /// <summary>
        /// [日付]コピーボタンのクリックイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
        private void dateCopyButton_Click(object sender, EventArgs e)
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

        /// <summary>
        /// [treeview]表示前のイベント
        /// </summary>
        /// <remarks>TreeView表示前にデータを読み込む</remarks>
        private void TreeView1_BeforeSelected()
        {
            List<Note> notes = new List<Note>();
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\dev\SaveReport\data.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        notes.Add(JsonConvert.DeserializeObject<Note>(line));
                    }

                    List<TreeNode> treeNodesList = new List<TreeNode>();
                    for (int i = 0; i < notes.Count; i++)
                    {
                        

                        List<TreeNode> treeNodesChild = new List<TreeNode>
                        {
                            new TreeNode(notes[i].Name)
                        };

                        //if (i > 0)
                        //{
                        //    var lastTitle = notes[i - 1].Title;
                        //    if (lastTitle == notes[i].Title)
                        //    {
                        //        treeNodesChild[i].Nodes.Add(notes[i].Name);
                        //    }

                        //}



                        TreeNode[] children = treeNodesChild.ToArray();
                       

                        TreeNode treeNodeGroup = new TreeNode(notes[i].Title, children);
                        
                        treeNodesList.Add(treeNodeGroup);
                    }

                    TreeNode[] treeNodeRoot = treeNodesList.ToArray();
                    treeView1.Nodes.AddRange(treeNodeRoot);

                    // 先頭のノードを展開する
                    treeView1.TopNode.Expand();
                    treeView1.SelectedNode = treeView1.Nodes[0].Nodes[0];
                    treeView1.Focus();
                    folderPathTextBox.Text = notes[0].Folder;
                    UrlTextBox.Text = notes[0].Url;
                    freeTextBox.Text = notes[0].Text;
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 画面右側の機能
        /// <summary>
        /// [ツリービュー]選択時のイベント
        /// </summary>
        /// <param name="sender">送信元</param>
        /// <param name="e">イベント</param>
        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
                string selectedName = treeView1.SelectedNode.Text;
                GetInfo(selectedName);
            
        }

        private void GetInfo(string name)
        {
            List<Note> notes = new List<Note>();
            try
            {
                using (StreamReader sr = new StreamReader(@"C:\dev\SaveReport\data.txt"))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        notes.Add(JsonConvert.DeserializeObject<Note>(line));
                    }
                }

                var a = notes.FindAll(x => x.Title == name);
                var num = notes.FindAll(x => x.Title == name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                string inputPath = folderPathTextBox.Text;
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
                if (!string.IsNullOrEmpty(UrlTextBox.Text))
                {
                    string url = UrlTextBox.Text;
                    System.Diagnostics.Process.Start($"{url}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(@"C:\dev\SaveReport")) 
            {
                Directory.CreateDirectory(@"C:\dev\SaveReport\");
            }

            using (StreamWriter sw = new StreamWriter(@"C:\dev\SaveReport\data.txt"))
            {
                Note note = new Note
                {
                    Title = treeView1.Nodes[0].Text,
                    Name = treeView1.SelectedNode.Text,
                    Folder = folderPathTextBox.Text,
                    Url = UrlTextBox.Text,
                    Text = freeTextBox.Text
                };

                string json = JsonConvert.SerializeObject(note);
                sw.WriteLine(json);
            }
            MessageBox.Show("保存しました。");
        }

        class Note 
        {
            public string Title { get; set; }
            public string Name { get; set; }
            public string Folder { get; set; }
            public string Url { get; set; }
            public string Text { get; set; }
        }

        private void singleLineCopyButton_Click(object sender, EventArgs e)
        {

        }
    }
}
