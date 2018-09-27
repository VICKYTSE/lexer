using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace 词法分析
{
    public partial class Form1 : Form
    {
        string sourceFileName;
        string builtFileName;
        static public string[] temp = new String[1000];
        analyseToken ayt = new analyseToken();
        public Form1()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;     //一次只能选取一个文件
            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.*)|*.*";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                sourceFileName = dialog.FileName;
                ayt.setSourceFileName(sourceFileName);
            }
            string[] lines = File.ReadAllLines(sourceFileName);
            // 在textBox1中显示文件内容
            char i = '0';
            int j = 0;
            foreach (string line in lines)
            {
                string str = ++i - '0' + " " + line + Environment.NewLine;
                textBox1.AppendText(str);
                temp[j++] = str;
            }           
        }

        private void buildFileButton_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            SaveFileDialog sfd = new SaveFileDialog();

            sfd.Filter = "文本文件文件（*.txt）|*.txt|word文件（*.doc）|*.doc";
            sfd.FilterIndex = 1;

            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                builtFileName = sfd.FileName.ToString(); //获得文件路径 
                ayt.setBuiltFileName(builtFileName);
                /* string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径*/
                ayt.readSourceFile();
            }
            string[] lines = File.ReadAllLines(builtFileName);
            // 在textBox1中显示文件内容
            foreach (string line in lines)
            {
                textBox2.AppendText(line + Environment.NewLine);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
