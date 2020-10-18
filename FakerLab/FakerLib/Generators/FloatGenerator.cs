using System;

namespace FakerLib.Generators
{
    class FloatGenerator:IValueGenerator
    {

        public object Generate(GeneratorContext context)
        {
            float value = (float)context.Random.Next(context.DownLimit, context.UpLimit) / context.Random.Next(context.DownLimit, context.UpLimit);
            return value;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(float);
        }

    }
}
