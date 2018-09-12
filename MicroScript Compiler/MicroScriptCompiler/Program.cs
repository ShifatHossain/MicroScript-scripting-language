using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroScriptCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Compiler com = new Compiler(@"E:/file.txt");
            com.compile();
        }
    }
}
