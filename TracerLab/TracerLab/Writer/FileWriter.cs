using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TracerLab.Writer
{
    public class FileWriter:IWriter
    {
        private const string path = "C:/BSUIR/a.txt";

        public void Write(string text)
        {
            File.WriteAllText(path,text);
        }

    }
}
