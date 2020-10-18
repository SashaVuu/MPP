using System;
using FakerLib.Generators;

namespace LongGeneratorPlugin
{
    class LongGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext context)
        {
            long value = (long)(context.Random.Next(context.DownLimit, context.UpLimit) * context.Random.Next(context.DownLimit, context.UpLimit));
            return value;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(long);
        }
    }
}
