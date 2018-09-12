using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace MicroScriptCompiler
{
    class Compiler
    {
        string fpath;
        byte[] file;
        string sfile;
        string[] lines;
        string[] lineparams;
        ArrayList parameters = new ArrayList();
        ArrayList exbarray = new ArrayList();
        public Compiler(string path)
        {
            fpath = path;
            
        }

        public void compile()
        {
            opener();
            line();
            reader();
            Writer();
        }


        void opener()
        {
            FileStream fs = File.Open(fpath, FileMode.Open);
            file = new byte[fs.Length];
            fs.Read(file, 0, Convert.ToInt32(fs.Length));
            sfile = System.Text.Encoding.ASCII.GetString(file);
            fs.Close();
            //fpath = "";
            file = new byte[0];
        }
        void line()
        {
            lines = sfile.Split(new string[]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            sfile = "";
            foreach (string par in lines)
            {
                lineparams = par.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string a in lineparams)
                {
                    parameters.Add(a);
                }
            }
        }
        void reader()
        {
            for (int i = 1;i <= parameters.Count;i++)
            {
                if ((string)parameters[i-1] == "NAME")
                {
                    NAME(i);
                }
                else if ((string)parameters[i-1] == "DEFINE")
                {
                    DEFINE(i);
                }
                else if ((string)parameters[i-1] == "PINTAGS")
                {
                    PINTAGS(i);
                }
                else if ((string)parameters[i-1] == "SEQUENCE")
                {
                    SEQUENCE(i);
                }
            }
            if ((exbarray.Count / 2) * 2 != exbarray.Count)
            {
                exbarray.Add('F');
            }
        }
        void NAME(int pos)
        {
            char[] w = name((string)parameters[pos]);
            foreach (char a in w)
            {
                exbarray.Add(a);
            }
        }
        void DEFINE(int pos)
        {
            int ind = pos;
            while ((string)parameters[ind] != "ENDDEF")
            {
                if ((string)parameters[ind] == "PIN")
                {
                    exbarray.Add('2');
                    exbarray.Add('3');
                    char[] a = num((string)parameters[ind + 1]);
                    foreach (char ab in a)
                    {
                        exbarray.Add(ab);
                    }
                }
                else if ((string)parameters[ind] == "FREQ")
                {
                    exbarray.Add('2');
                    exbarray.Add('3');
                    char[] a = num((string)parameters[ind + 1]);
                    foreach (char ab in a)
                    {
                        exbarray.Add(ab);
                    }
                }
                ind++;
            }
        }
        void PINTAGS(int pos)
        {
            int ind = pos;
            ArrayList n = new ArrayList();
            ArrayList t = new ArrayList();
            while ((string)parameters[ind] != "ENDTAG")
            {
                ind++;
            }
            //int space = ind - pos + 1;
            for (int i = pos + 1; i <= ind; i = i + 3)
            {
                n.Add((string)parameters[i]);
            }
            for (int i = pos + 2; i <= ind; i = i + 3)
            {
                t.Add((string)parameters[i]);
            }
            exbarray.Add('2');
            exbarray.Add('3');
            char[] nc;
            string ns = "";
            for (int i = 1; i <= n.Count; i++)
            {
                nc = name((string)n[i - 1]);
                foreach (char a in nc)
                {
                    exbarray.Add(a);
                }
                exbarray.Add('5');
                exbarray.Add('E');
                ns = (string)t[i - 1];
                if (ns == "I")
                {
                    exbarray.Add('0');
                    exbarray.Add('0');
                }
                else if (ns == "O")
                {
                    exbarray.Add('0');
                    exbarray.Add('1');
                }
                else if (ns == "IO")
                {
                    exbarray.Add('0');
                    exbarray.Add('2');
                }
                else if (ns == "CLK")
                {
                    exbarray.Add('0');
                    exbarray.Add('3');
                }
                if (i != n.Count)
                {
                    exbarray.Add('2');
                    exbarray.Add('4');
                }
            }
        }
        void SEQUENCE(int pos)
        {
            exbarray.Add('2');
            exbarray.Add('3');
            int ind = pos;
            while ((string)parameters[ind] != "ENDSEQ")
            {
                if ((string)parameters[ind] == "START")
                {
                    if (ind != pos)
                    {
                        exbarray.Add('2');
                        exbarray.Add('4');
                    }
                    exbarray.Add('0');
                    exbarray.Add('A');
                    exbarray.Add('5');
                    exbarray.Add('E');
                }
                else if ((string)parameters[ind] == "BIT")
                {
                    if (ind != pos)
                    {
                        exbarray.Add('2');
                        exbarray.Add('4');
                    }
                    exbarray.Add('0');
                    exbarray.Add('B');
                    exbarray.Add('5');
                    exbarray.Add('E');
                }
                else if ((string)parameters[ind] == "BYTE")
                {
                    if (ind != pos)
                    {
                        exbarray.Add('2');
                        exbarray.Add('4');
                    }
                    exbarray.Add('0');
                    exbarray.Add('C');
                    exbarray.Add('5');
                    exbarray.Add('E');
                }
                else if ((string)parameters[ind] == "END")
                {
                    if (ind != pos)
                    {
                        exbarray.Add('2');
                        exbarray.Add('4');
                    }
                    exbarray.Add('0');
                    exbarray.Add('D');
                    exbarray.Add('5');
                    exbarray.Add('E');
                }
                else if ((string)parameters[ind] == "GENERAL")
                {
                    if (ind != pos)
                    {
                        exbarray.Add('2');
                        exbarray.Add('4');
                    }
                    exbarray.Add('0');
                    exbarray.Add('E');
                    exbarray.Add('5');
                    exbarray.Add('E');
                    char[] n = name((string)parameters[ind + 1]);
                    foreach (char a in n)
                    {
                        exbarray.Add(a);
                    }
                    exbarray.Add('5');
                    exbarray.Add('E');
                }

                

                if ((string)parameters[ind] == "ESEQ")
                {
                    exbarray.Add('0');
                    char[] n = name((string)parameters[ind +1]);
                    foreach(char a in n)
                    {
                        exbarray.Add(a);
                    }
                }
                else if ((string)parameters[ind] == "SET")
                {
                    exbarray.Add('1');
                    char[] nu = num((string)parameters[ind + 1]);
                    foreach (char a in nu)
                    {
                        exbarray.Add(a);
                    }
                    char[] sd = SDATA(ind + 2);
                    foreach (char a in sd)
                    {
                        exbarray.Add(a);
                    }
                }
                else if ((string)parameters[ind] == "INV")
                {
                    exbarray.Add('2');
                    char[] nu = num((string)parameters[ind + 1]);
                    foreach (char a in nu)
                    {
                        exbarray.Add(a);
                    }
                }
                else if ((string)parameters[ind] == "SETB")
                {
                    exbarray.Add('3');
                    char[] nu = num((string)parameters[ind + 1]);
                    foreach (char a in nu)
                    {
                        exbarray.Add(a);
                    }
                    exbarray.Add(DATA(ind + 2));
                }
                else if ((string)parameters[ind] == "COND")
                {
                    exbarray.Add('4');
                    char[] opa = COND_OP(ind + 1);
                    foreach (char a in opa)
                    {
                        exbarray.Add(a);
                    }
                    string c = (string)parameters[ind + 2];
                    if (c == "L")
                    {
                        exbarray.Add('0');
                    }
                    else if (c == "G")
                    {
                        exbarray.Add('1');
                    }
                    else if (c == "LE")
                    {
                        exbarray.Add('3');
                    }
                    else if (c == "GE")
                    {
                        exbarray.Add('4');
                    }
                    else if (c == "NE")
                    {
                        exbarray.Add('5');
                    }
                    else
                    {
                        exbarray.Add('2');
                    }
                    char[] opb = COND_OP(ind + 3);
                    foreach (char a in opb)
                    {
                        exbarray.Add(a);
                    }
                    char[] n = name((string)parameters[ind + 4]);
                    foreach (char a in n)
                    {
                        exbarray.Add(a);
                    }
                }
                else if ((string)parameters[ind] == "SETHL")
                {
                    exbarray.Add('5');
                    char[] nu = num((string)parameters[ind + 1]);
                    foreach (char a in nu)
                    {
                        exbarray.Add(a);
                    }
                    exbarray.Add(HLDATA(ind + 2));
                }
                else if ((string)parameters[ind] == "WAIT")
                {
                    exbarray.Add('6');
                    char[] sd = SDATA(ind + 1);
                    foreach (char a in sd)
                    {
                        exbarray.Add(a);
                    }
                    char[] nu = num((string)parameters[ind + 2]);
                    foreach (char a in nu)
                    {
                        exbarray.Add(a);
                    }
                    string ene = (string)parameters[ind + 3];
                    if (ene == "NE")
                    {
                        exbarray.Add('0');
                    }
                    else
                    {
                        exbarray.Add('1');
                    }
                }
                else if ((string)parameters[ind] == "WAITCLK")
                {
                    exbarray.Add('7');
                    char[] nu = num((string)parameters[ind + 1]);
                    foreach (char a in nu)
                    {
                        exbarray.Add(a);
                    }
                }
                else if ((string)parameters[ind] == "WAITTIME")
                {
                    exbarray.Add('8');
                    string unit = (string)parameters[ind + 1];
                    if (unit == "NS")
                    {
                        exbarray.Add('0');
                    }
                    else if (unit == "MS")
                    {
                        exbarray.Add('1');
                    }
                    else
                    {
                        exbarray.Add('2');
                    }
                    char[] nu = num((string)parameters[ind + 2]);
                    foreach (char a in nu)
                    {
                        exbarray.Add(a);
                    }
                }
                else if ((string)parameters[ind] == "RESET")
                {
                    exbarray.Add('9');
                }
                else if ((string)parameters[ind] == "BREAK")
                {
                    exbarray.Add('A');
                    string a = (string)parameters[ind + 1];
                    if (a == "1")
                    {
                        exbarray.Add('1');
                    }
                    else
                    {
                        exbarray.Add('0');
                    }
                }
                ind++;
            }
        }

        char[] name(string name)
        {
            string namecon = "";
            char[] chars = name.ToCharArray();
            foreach (char a in chars)
            {
                namecon = namecon + named(a);
            }
            namecon = namecon + "FF";
            return namecon.ToCharArray();
        }
        char[] num(string num)
        {
            string onum = num + "A";
            return onum.ToCharArray();
        }
        char[] SDATA(int pos)
        {
            string op = "";
            //char[] ins = (string)parameters[pos].ToString().ToCharArray();
            string para = (string)parameters[pos];
            char[] ins = new char[para.Length];
            for (int i = 0; i <= (para.Length - 1); i++)
            {
                ins[i] = para[i];
            }
            if ((string)parameters[pos] == "0" || (string)parameters[pos] == "1")
            {
                op = "0" + (string)parameters[pos];
            }
            else if (ins[0].ToString() == "L" || ins[0].ToString() == "M" || ins[0].ToString() == "N" || ins[0].ToString() == "P")
            {
                op = "1";
                if (ins[0].ToString() == "L")
                {
                    op = op + "0" + ins[2];
                }
                else if ((string)parameters[pos] == "M")
                {
                    op = op + "1" + ins[2];
                }
                else if ((string)parameters[pos] == "N")
                {
                    op = op + "2" + ins[2];
                }
                else
                {
                    string num = "";
                    for (int i = 2; i <= (ins.Length - 1); i++)
                    {
                        num = num + ins[i];
                    }
                    num = num + "A";
                    op = op + "3" + num;
                }
            }
            else if ((string)parameters[pos] == "BITD")
            {
                op = "2";
            }
            return op.ToCharArray();
        }
        char DATA(int pos)
        {
            if ((string)parameters[pos] == "L")
            {
                return '0';
            }
            else if ((string)parameters[pos] == "N")
            {
                return '2';
            }
            else
            {
                return '1';
            }
        }
        char HLDATA(int pos)
        {
            if ((string)parameters[pos] == "LH")
            {
                return '0';
            }
            else if ((string)parameters[pos] == "LL")
            {
                return '1';
            }
            else if ((string)parameters[pos] == "ML")
            {
                return '3';
            }
            else if ((string)parameters[pos] == "NH")
            {
                return '4';
            }
            else if ((string)parameters[pos] == "NL")
            {
                return '5';
            }
            else
            {
                return '2';
            }
        }
        char[] COND_OP(int pos)
        {
            string a = (string)parameters[pos];
            string ou = "";
            char[] n = a.ToCharArray();
            if (a == "POS")
            {
                ou = "1";
            }
            else if (a == "DLENGTH")
            {
                ou = "2";
            }
            else if (n[0] == 'V')
            {
                string num = "";
                for (int i = 1; i <= (n.Length - 1); i++)
                {
                    num = num + n[i];
                }
                num = num + "A";
                ou = "3" + num;
            }
            else
            {
                string o2 = new string(SDATA(pos));
                ou = "0" + o2;
            }
            return ou.ToCharArray();
        }
        string named(char c)
        {
            string re = "";
            if (c == 'A')
            {
                re = "00";
            }
            else if (c == 'B')
            {
                re = "01";
            }
            else if (c == 'C')
            {
                re = "02";
            }
            else if (c == 'D')
            {
                re = "03";
            }
            else if (c == 'E')
            {
                re = "04";
            }
            else if (c == 'F')
            {
                re = "05";
            }
            else if (c == 'G')
            {
                re = "06";
            }
            else if (c == 'H')
            {
                re = "07";
            }
            else if (c == 'I')
            {
                re = "08";
            }
            else if (c == 'J')
            {
                re = "09";
            }
            else if (c == 'K')
            {
                re = "0A";
            }
            else if (c == 'L')
            {
                re = "0B";
            }
            else if (c == 'M')
            {
                re = "0C";
            }
            else if (c == 'N')
            {
                re = "0D";
            }
            else if (c == 'O')
            {
                re = "0E";
            }
            else if (c == 'P')
            {
                re = "10";
            }
            else if (c == 'Q')
            {
                re = "11";
            }
            else if (c == 'R')
            {
                re = "12";
            }
            else if (c == 'S')
            {
                re = "13";
            }
            else if (c == 'T')
            {
                re = "14";
            }
            else if (c == 'U')
            {
                re = "15";
            }
            else if (c == 'V')
            {
                re = "16";
            }
            else if (c == 'W')
            {
                re = "17";
            }
            else if (c == 'X')
            {
                re = "18";
            }
            else if (c == 'Y')
            {
                re = "19";
            }
            else if (c == 'Z')
            {
                re = "1A";
            }
            else if (c == '0')
            {
                re = "1B";
            }
            else if (c == '1')
            {
                re = "1C";
            }
            else if (c == '2')
            {
                re = "1D";
            }
            else if (c == '3')
            {
                re = "1E";
            }
            else if (c == '4')
            {
                re = "20";
            }
            else if (c == '5')
            {
                re = "21";
            }
            else if (c == '6')
            {
                re = "22";
            }
            else if (c == '7')
            {
                re = "23";
            }
            else if (c == '8')
            {
                re = "24";
            }
            else if (c == '9')
            {
                re = "25";
            }
            return re;
        }

        void Writer()
        {
            fpath = fpath + ".cus";
            file = new byte[exbarray.Count /2];
            for(int i=0;i<=(exbarray.Count/2-1);i++)
            {
                file[i] = (byte)(hval((char)exbarray[i * 2]) * 16 + hval((char)exbarray[i * 2 + 1]));
            }
            FileStream fo = new FileStream(fpath, FileMode.Create);
            fo.Write(file, 0, file.Length);
            fo.Close();
        }
        int hval(char hex)
        {
            int a = 0;
            if (hex == 'F')
            {
                a = 15;
            }
            else if (hex == 'E')
            {
                a = 14;
            }
            else if (hex == 'D')
            {
                a = 13;
            }
            else if (hex == 'C')
            {
                a = 12;
            }
            else if (hex == 'B')
            {
                a = 11;
            }
            else if (hex == 'A')
            {
                a = 10;
            }
            else if (hex == '9')
            {
                a = 9;
            }
            else if (hex == '8')
            {
                a = 8;
            }
            else if (hex == '7')
            {
                a = 7;
            }
            else if (hex == '6')
            {
                a = 6;
            }
            else if (hex == '5')
            {
                a = 5;
            }
            else if (hex == '4')
            {
                a = 4;
            }
            else if (hex == '3')
            {
                a = 3;
            }
            else if (hex == '2')
            {
                a = 2;
            }
            else if (hex == '1')
            {
                a = 1;
            }
            else
            {
                a = 0;
            }
            return a;
        }

    }
}
