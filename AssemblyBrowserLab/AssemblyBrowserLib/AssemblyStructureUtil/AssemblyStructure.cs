using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
            //Когда грузим консоль может вернуть nulls
            try
            {
                //Убираем классы и тд сгенерированные компилятором
                types = assembly.GetTypes().Where(item => Attribute.GetCustomAttribute(item, typeof(CompilerGeneratedAttribute)) == null).ToArray();
            }
            catch(ReflectionTypeLoadException e)
            {
               types = e.Types.Where(t => t != null).ToArray();
            }

            //Проходимся по всем классам,структурам и тд, что бы сформировать namespaces
            for (int i=0;i<types.Length;i++) 
            {
                AssemblyNamespace ass = GetOrAddNamespace(types[i]);
                ass.AddType(types[i]);
            }

        }

        //Создаем список namespaces
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
