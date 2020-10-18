using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace FakerLib.Generators
{
    public class UriGenerator:IValueGenerator
    {
        public object Generate(GeneratorContext context)
        {
            string s = context.Faker.Create<string>();
            return new Uri("http://www."+s+".com");
        }
        public bool CanGenerate(Type type)
        {
            return type == typeof(Uri);
        }
    }
}
