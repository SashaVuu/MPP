using System;

namespace FakerLib.Generators
{
    class IntGenerator:IValueGenerator
    {
        
        public object Generate(GeneratorContext context)
        {
            int value = context.Random.Next(context.DownLimit, context.UpLimit);
            return value;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }

    }
}
