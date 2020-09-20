using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLab.Writer
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string text)
        {
            Console.WriteLine(text);
        }

    }
}
