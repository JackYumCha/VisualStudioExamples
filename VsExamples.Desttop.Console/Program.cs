using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VsConsole = System.Console;
using VsExamples.Standard;

namespace VsExamples.Desttop.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();

            VsConsole.WriteLine(string.Join(", ", args));
        }
    }
}
