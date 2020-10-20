using AssemblyBrowserLib.AssemblyStructureUtil;
using System;

namespace AssemblyBrowserLab
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyInfo.LoadAssemblyByPath("C:/Users/shell/Documents/GitHub/MPP/AssemblyBrowserLab/AssemblyBrowserLib/bin/Debug/netstandard2.0/AssemblyBrowserLib");
            Console.WriteLine(AssemblyInfo.assemblyStructure);
        }
    }
}
