using FakerLib.Generators;
using System;

namespace DoubleGeneratorPlugin
{
    public class DoubleGenerator : IValueGenerator
    {
        public object Generate(GeneratorContext context)
        {
            double value = (double)7.456;
            return value;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(double);
        }

    }
}
