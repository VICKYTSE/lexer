using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Collections;

namespace 词法分析
{
    class analyseToken
    {
        protected string sourceFileName;
        protected string builtFileName;

        //Stack st = new Stack();         //用于括号匹配的栈
        char[] prog = new char [1000];  //暂存输入字符
        char[] token = new char[20];

        char ch;
        int p = 0;
        int sym = 0;
        int n;
        int errorLine;    //当前扫描的行号
        string[] keyword = { "if", "else", "while", "for", "do", "return",
         "int" ,"main","void","double","float","case","for","do","short","static",
        "true","false","try","delete","class","break","bool","goto","default","using",
        "new","continue","switch","throw","unsigned","signed","sizeof"};  //关键词数组

        public analyseToken()     //构造函数
        {
            Array.Clear(prog,(char)0,1000);
        }
        public void setSourceFileName(string file)
        {
            sourceFileName = file;
        }
        public void setBuiltFileName(string file)
        {
            builtFileName = file;
        }
        protected void GetToken()
        {
            ch = prog[p++];
            while (ch == ' ' || ch == '\n' || ch == '\t' ||(int)ch == 13||(int)ch==9) { ch = prog[p++]; }

            for (n = 0; n < 20; n++)
            {
                token[n] = '\0';
            }
            n = 0;

            if ((int)ch == 0)
            {

                return;
            }
            if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z')||ch == '_')
            {
                sym = 1;
                do
                {
                    token[n++] = ch;
                    ch = prog[p++];
                } while ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z')|| (ch >= '0' && ch <= '9')||ch == '_');
                sym = 2;

                string str1 = null;

                for (int i = 0; token[i] != '\0'; i++)
                {

                    str1 += token[i];
                }
                for (n = 0; n<33;n++)
                {
                    if (String.Compare(str1, keyword[n]) == 0)                     //关键词匹配
                    {
                        sym = n + 23;
                    }
                }

                p--;
                
            }
            //else if (ch == '{'||ch == '}' || ch == '(' || ch == ')' || ch == '[' || ch == ']')                             //括号处理
            //{
            //    if (ch == '{')
            //    {
            //        sym = 3;
            //        //st.Push(ch);
            //        token[0] = ch;
            //    }
            //    else if (ch == '}')
            //    {
            //        //if ((char)st.Peek() == '{')
            //        //{
            //            sym = 4;
            //            //st.Pop();
            //            token[0] = ch;
            //        //}
            //        /*else
            //        {
            //            sym = -1;                   //括号不匹配
            //        }      */                               
            //    }
            //    else if (ch == '(')
            //    {
            //        sym = 5;
            //        //st.Push(ch);
            //        token[0] = ch;
            //    }
            //    else if (ch == ')')
            //    {
            //        //if ((char)st.Peek() == '(')
            //        //{
            //            sym = 6;
            //            //st.Pop();
            //            token[0] = ch;
            //       /* }
            //        else
            //        {
            //            sym = -1;                   //括号不匹配
            //        }*/
            //    }
            //    else if (ch == '[')
            //    {
            //        sym = 7;
            //        //st.Push(ch);
            //        token[0] = ch;
            //    }
            //    else if (ch == ']')
            //    {
            //       /* if ((char)st.Peek() == '[')
            //        {*/
            //            sym = 8;
            //            //st.Pop();
            //            token[0] = ch;
            //      /*  }
            //        else
            //        {
            //            sym = -1;                   //括号不匹配
            //        }*/
            //    }

            //    //do
            //    //{
            //    //    ch = prog[p++];
            //    //} while (ch != '}');
            //    //sym = -1;
            //    //Console.WriteLine("sym=" + sym + "and ch=" + ch);
            //    return;
            //}
            //else if (ch == '=' )
            //{
            //    token[n++] = ch;
            //    ch = prog[p++];
            //    if (ch == '=')      //==
            //    {
            //        token[n++] = ch;
            //        sym = 9;
            //    }
            //    else
            //    {
            //        sym=19;p--;
            //    }
            //    //Console.WriteLine("sym=" + sym + "and ch=" + ch);
            //    return;
            //}
            else if (ch == '!')
            {
                token[n++] = ch;
                ch = prog[p++];
                if (ch == '=')      //!=
                {
                    token[n++] = ch;
                    sym = 10;
                }
                else
                {
                    sym = -1;
                }
                //Console.WriteLine("sym=" + sym + "and ch=" + ch);
                return;
            }
            else if (ch == '>'||ch=='<')
            {
                token[n++] = ch;
                char c = ch;
                ch = prog[p++];
                if (ch == '=')      
                {
                    if (c == '<') sym = 11;    //<=
                    else sym = 12;             //>=
                    token[n++] = ch;
                }
                else
                {
                    if (c == '<') sym = 20;
                    else sym = 21;
                    p--;
                }
                //Console.WriteLine("sym=" + sym + "and ch=" + ch);
                return;
            }
            else if (ch >= '0' && ch <= '9')            
            {
                sym = 13;
                do
                {
                    token[n++] = ch;
                    ch = prog[p++];
                } while (ch >= '0' && ch <= '9');
                sym = 14;

                if (ch != ' ' && ch != '+' && ch != '-' && ch != '*' && ch != '/' && ch != ';' && ch != '\n' && ch != '\t' && (int)ch != 13 && (int)ch != 9 && ch != '{' && ch != '(' && ch != '[' && ch != '<' && ch != '>' && ch != '=' && ch != '}' && ch != ')' && ch != ']')
                {
                    do
                    {
                        token[n++] = ch;
                        ch = prog[p++];
                    } while (!(ch == ' ' || ch == '\n' || ch == '\t' || (int)ch == 13 || (int)ch == 9));
                    sym = -1;
                    LocateError();
                }
                 p--;

                //Console.WriteLine("sym=" + sym + "and ch=" + ch);
                return;
            }
            else
            {
                switch (ch)
                {
                    case '{': sym = 3; token[0] = ch; break;
                    case '}': sym = 4; token[0] = ch; break;
                    case '[': sym = 5; token[0] = ch; break;
                    case ']': sym = 6; token[0] = ch; break;
                    case '(': sym = 7; token[0] = ch; break;
                    case ')': sym = 8; token[0] = ch; break;
                    case '+': sym = 15; token[0] = ch; break;
                    case '-': sym = 16; token[0] = ch; break;
                    case '*': sym = 17; token[0] = ch; break;
                    case '/': sym = 18; token[0] = ch; break;
                    case '=': sym = 19; token[0] = ch; break;
                    case '<': sym = 20; token[0] = ch; break;
                    case '>': sym = 21; token[0] = ch; break;
                    case ';': sym = 22; token[0] = ch; break;
                    default: sym = -2; MessageBox.Show("The input contains illegal characters."); break;
                }
            }
            
        }

            public void readSourceFile()     
        {
            FileStream aFile = new FileStream(sourceFileName, FileMode.Open);
            StreamReader sr = new StreamReader(aFile);
            FileStream fs = new FileStream(builtFileName, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            do
            {
                ch = (char)sr.Read();
                if (ch == '/' && sr.Peek() == '/') { string c = sr.ReadLine(); ch = '\n';}            //忽略//后的注释
                if (ch == '/' && sr.Peek() == '*')
                {
                    do
                    {
                        ch = (char)sr.Read();
                    } while (!(ch == '*' && sr.Peek() == '/'));
                    ch = (char)sr.Read(); ch = (char)sr.Read();                   //忽略/* */之间的注释
                }
                prog[p++] = ch;

            } while (sr.Peek() >= 0);
            p = 0;
            do
            {
                GetToken();
                if ((int)ch == 0) break;
                string str1 = null;
                for (int i=0;token[i]!='\0';i++)
                {
                    str1 += token[i];
                }
                if (str1 != null)
                {
                    //Console.WriteLine("sym"+sym);
                    switch (sym)
                    {
                        case -1: string str2 = "Line"+errorLine.ToString()+ " **Error occurs in "+str1 ; sw.WriteLine(str2); Console.WriteLine(str2); break;
                        case -2: string str3 = '(' + sym.ToString() + ',' + "Error" + ')'; sw.WriteLine(str3); Console.WriteLine(str3); break;
                        default: string str = '(' + sym.ToString() + ',' + str1 + ')'; sw.WriteLine(str); Console.WriteLine(str); break;
                    }
                }
            } while (ch>0);
            sr.Close();
            sw.Close();
            Console.WriteLine("File closed.");
        }

        void LocateError()
        {
            string str1 = null;
            for (int i = 0; token[i] != '\0'; i++)
            {
                str1 += token[i];
            }
            int j = 0;
            while (!string.IsNullOrWhiteSpace(Form1.temp[j]))
            {
                int i = Form1.temp[j++].IndexOf(str1);
                if (i > 0) { errorLine = j; break; }
            }
        }



    }
}
