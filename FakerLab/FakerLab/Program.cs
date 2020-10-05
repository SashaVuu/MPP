using FakerLab.FakerLib;
using FakerLab.Generators;
using FakerLab.TestClasses;
using System;
using System.Reflection;

namespace FakerLab
{

    class Program
    {
        static object CreateMyObject(Type myType, Type[] parameters, object[] values)
        {
            // reflection (получаем конструктор по типам) 
            ConstructorInfo info = myType.GetConstructor(parameters);

            // reflection (создаем объект, вызывая конструктор) 
            object myObj = info.Invoke(values);

            // result 
            return myObj;
        }

        static void Main(string[] args)
        {
            Faker faker = new Faker();
            Book a = faker.Create<Book>();
            Console.WriteLine(a.Aw.Method());
            Console.WriteLine(a);



            Generator gen = new Generator();
            Console.WriteLine(gen.GenerateInt());
            Console.WriteLine(gen.GenerateDouble().ToString());
            Console.WriteLine(gen.GenerateFloat().ToString());
            Console.WriteLine(gen.GenerateLong().ToString());
            Console.WriteLine(gen.GenerateChar());
            Console.WriteLine(gen.GenerateString());
        }
    }
}
