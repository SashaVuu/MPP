using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TracerLab.Writer
{
    public class FileWriter:IWriter
    {
        private string Path;

        public FileWriter()
        {
            Path = "C:/BSUIR/a.txt";
        }

        public void Write(string text)
        {
            File.WriteAllText(Path,text);
        }

        public void SetPath(string path)
        {
            Path = path;
        }

    }
}
