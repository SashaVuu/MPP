using FakerLib.FakerUtil;
using System;

namespace FakerLib.Generators
{
    // Отдельный тип для параметров генератора упрощает добавление новых параметров
    // в контекст без изменения сигнатуры функции.
    public class GeneratorContext
    {
        // Для сохранения состояния генератора псевдослучайных чисел
        // и получения различных значений при нескольких последовательных вызовах.
        public Random Random { get; }
        public int UpLimit = 200;
        public int DownLimit = 1;

        public Type TargetType { get; }

        // Даем возможность генератору использовать все возможности Faker.
        // Необходимо для создания коллекций произвольных объектов,
        // но может удобно и в некоторых других случаях.
        public IFaker Faker { get; }

        public GeneratorContext(Random random, Type targetType, IFaker faker)
        {
            Random = random;
            TargetType = targetType;
            Faker = faker;
        }
    }
}
