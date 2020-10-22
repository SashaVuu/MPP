
using FakerLib.FakerUtil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FakerLib.Generators
{
    public class ListGenerator:IValueGenerator
    {
        
        private int downLimit = 1;
        private int upLimit = 4;


        public object Generate(GeneratorContext context)
        {
            Type paramType = context.TargetType.GetGenericArguments()[0];
            int listSize = context.Random.Next(downLimit, upLimit);

            IList list= (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(paramType));

            for (int i=0;i<listSize;i++)
            {
                list.Add(((Faker)context.Faker).Create(paramType));
            }
            return list;
        }

        public bool CanGenerate(Type type)
        {
            return type.Name.Contains("List");
        }
    }
}
