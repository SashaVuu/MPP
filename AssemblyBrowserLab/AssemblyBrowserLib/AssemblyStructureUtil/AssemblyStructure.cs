using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AssemblyBrowserLib.AssemblyStructureUtil
{
    public class AssemblyStructure
    {
        public string FullName { get; set; } 
        public List<AssemblyNamespace> nameSpaces = new List<AssemblyNamespace>();


        public AssemblyStructure(Assembly assembly)
        {
            Type[] types = null;
            try
            {
                types = assembly.GetTypes();
            }
            catch(ReflectionTypeLoadException e)
            {
                /*Type[] t = e.Types;
                int k = 0;
                for (int i=0;i<=t.Length;i++)
                {
                    if (t[i] != null)
                    {
                        types[k] = t[i];
                        k++;
                    }
                }*/
                types = e.Types.Where(t => t != null).ToArray();

            }
            for (int i=0;i<types.Length;i++) 
            {
                AssemblyNamespace ass = GetOrAddNamespace(types[i]);
                ass.AddType(types[i]);
            }
        }

        private AssemblyNamespace GetOrAddNamespace(Type type)
        {
            AssemblyNamespace result = null;

            foreach (AssemblyNamespace nameSpace in nameSpaces) 
            {
                if (nameSpace.FullName==type.Namespace) 
                {
                    result=nameSpace;
                }
            }
            if (result == null) 
            {
                result = new AssemblyNamespace(type.Namespace);
                nameSpaces.Add(result);
            }
            return result;
        }


    }
}
