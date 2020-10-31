using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib.AssemblyStructureUtil
{
    public static class AssemblyInfo
    {
        public static AssemblyStructure assemblyStructure;

        public static void LoadAssemblyByPath(string path)
        {
            Assembly loadedAssembly = Assembly.LoadFrom(path);
            assemblyStructure = new AssemblyStructure(loadedAssembly);
        }


    }
}
