using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLib.Generators
{
    public class StringGenerator:IValueGenerator
    {
      

        public object Generate(GeneratorContext context)
        {
            string currentString = "";
            for (int i = 0; i <= context.Random.Next(context.DownLimit, context.UpLimit); i++)
            {
                currentString += (char)context.Random.Next('A', 'Z');
            }
            return currentString;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }

    }

}
