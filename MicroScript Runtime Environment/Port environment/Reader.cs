using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace Port_environment
{
    class Reader
    {
        string fstring = "";
        public string fname = "";
        public int pinc = 0;
        public int freq = 0;
        string pins = "";
        string segmentss = "";
        ArrayList pinl = new ArrayList();
        ArrayList segments = new ArrayList();
        ArrayList bitseq = new ArrayList();
        ArrayList byteseq = new ArrayList();
        public Reader(string path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                byte[] file = new byte[fs.Length - 1];
                fs.Read(file, 0, 0);
                hexwriter(file);
                fs.Close();
                file = new byte[1];
                divider();
            }
            catch
            {
                Error d = new Error(fstring);
                d.Show();
            }
        }

        void hexwriter(byte[] file)
        {
            foreach (byte a in file)
            {
                fstring = fstring + byteval(a);
            }
        }
        string byteval(byte value)
        {
            BitArray b = new BitArray(new byte[] {value});
            string H = tf(b[0].ToString()) + tf(b[1].ToString()) + tf(b[2].ToString()) + tf(b[3].ToString());
            string L = tf(b[4].ToString()) + tf(b[5].ToString()) + tf(b[6].ToString()) + tf(b[7].ToString());

            int hi = Convert.ToInt32(H, 2);
            int li = Convert.ToInt32(L, 2);

            return hexval(hi).ToString() + hexval(li).ToString();
        }
        string tf(string a)
        {
            if (a == "true")
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        char hexval(int hl)
        {
            char o;
            if (hl == 0)
            {
                o = '0';
            }
            else if (hl == 1)
            {
                o = '1';
            }
            else if (hl == 2)
            {
                o = '2';
            }
            else if (hl == 3)
            {
                o = '3';
            }
            else if (hl == 4)
            {
                o = '4';
            }
            else if (hl == 5)
            {
                o = '5';
            }
            else if (hl == 6)
            {
                o = '6';
            }
            else if (hl == 7)
            {
                o = '7';
            }
            else if (hl == 8)
            {
                o = '8';
            }
            else if (hl == 9)
            {
                o = '9';
            }
            else if (hl == 10)
            {
                o = 'A';
            }
            else if (hl == 11)
            {
                o = 'B';
            }
            else if (hl == 12)
            {
                o = 'C';
            }
            else if (hl == 13)
            {
                o = 'D';
            }
            else if (hl == 14)
            {
                o = 'E';
            }
            else
            {
                o = 'F';
            }
            return o;
        }

        string name(string hexstr)
        {
            string nam = "";
            string s = "";
            for(int i = 0;i<=((hexstr.Length/2) -1);i++)
            {
                s = hexstr[i].ToString() + hexstr[i + 1].ToString();
                if (s != "FF")
                {
                    nam = nam + nameval(s).ToString();
                }
            }
            return nam;
        }
        char nameval(string anhex)
        {
            char o;
            if (anhex == "00")
            {
                o = 'A';
            }
            else if (anhex == "01")
            {
                o = 'B';
            }
            else if (anhex == "02")
            {
                o = 'C';
            }
            else if (anhex == "03")
            {
                o = 'D';
            }
            else if (anhex == "04")
            {
                o = 'E';
            }
            else if (anhex == "05")
            {
                o = 'F';
            }
            else if (anhex == "06")
            {
                o = 'G';
            }
            else if (anhex == "07")
            {
                o = 'H';
            }
            else if (anhex == "08")
            {
                o = 'I';
            }
            else if (anhex == "09")
            {
                o = 'J';
            }
            else if (anhex == "0A")
            {
                o = 'K';
            }
            else if (anhex == "0B")
            {
                o = 'L';
            }
            else if (anhex == "0C")
            {
                o = 'M';
            }
            else if (anhex == "0D")
            {
                o = 'N';
            }
            else if (anhex == "0E")
            {
                o = 'O';
            }
            else if (anhex == "10")
            {
                o = 'P';
            }
            else if (anhex == "11")
            {
                o = 'Q';
            }
            else if (anhex == "12")
            {
                o = 'R';
            }
            else if (anhex == "13")
            {
                o = 'S';
            }
            else if (anhex == "14")
            {
                o = 'T';
            }
            else if (anhex == "15")
            {
                o = 'U';
            }
            else if (anhex == "16")
            {
                o = 'V';
            }
            else if (anhex == "17")
            {
                o = 'W';
            }
            else if (anhex == "18")
            {
                o = 'X';
            }
            else if (anhex == "19")
            {
                o = 'Y';
            }
            else if (anhex == "1A")
            {
                o = 'Z';
            }
            else if (anhex == "1B")
            {
                o = '0';
            }
            else if (anhex == "1C")
            {
                o = '1';
            }
            else if (anhex == "1D")
            {
                o = '2';
            }
            else if (anhex == "1E")
            {
                o = '3';
            }
            else if (anhex == "20")
            {
                o = '4';
            }
            else if (anhex == "21")
            {
                o = '5';
            }
            else if (anhex == "22")
            {
                o = '6';
            }
            else if (anhex == "23")
            {
                o = '7';
            }
            else if (anhex == "24")
            {
                o = '8';
            }
            else
            {
                o = '9';
            }
            return o;
        }

        int num(string n)
        {
            string o = "";
            foreach (char a in n)
            {
                if (a != 'A')
                {
                    o = o + a.ToString();
                }
            }
            return Convert.ToInt32(o, 10);
        }


        void divider()
        {
            string[] d = fstring.Split(new string[]{"23"}, StringSplitOptions.RemoveEmptyEntries);
            fname = name(d[0]);
            pinc = num(d[1]);
            freq = num(d[2]);
            pins = d[3];
            segmentss = d[4];

            string[] par = pins.Split(new string[]{"24"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string a in par)
            {
                string[] s = a.Split(new string[] {"5E"}, StringSplitOptions.RemoveEmptyEntries);
                pinl.Add(name(s[0]));
                pinl.Add(s[1]);
            }
            string[] sar = segmentss.Split(new string[]{"24"}, StringSplitOptions.RemoveEmptyEntries);
            foreach (string a in sar)
            {
                segments.Add(a);
            }
            pins = "";
            segmentss = "";
        }

        void initiatebit()
        {

        }
    }
}
