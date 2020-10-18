using System;

namespace FakerLib.Generators
{
    public class CharGenerator : IValueGenerator
    {
       
        public object Generate(GeneratorContext context)
        {
            return (char)context.Random.Next('A', 'Z');
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(char);
        }
    }
}
