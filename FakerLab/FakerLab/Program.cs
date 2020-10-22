using FakerLab.TestClasses;
using FakerLib.FakerUtil;
using FakerLib.Generators;
using System;
using System.Collections.Generic;


namespace FakerLab
{

    class Program
    {
 
        static void Main(string[] args)
        {
            Faker faker = new Faker();
            Book a = faker.Create<Book>();
            MyStruct ss = faker.Create<MyStruct>();

            double d = faker.Create<double>();
            A b = faker.Create<A>();
            Console.WriteLine(a.PageInst.Method());
            Console.WriteLine(a.Name);
            Console.WriteLine(a.Price);
            Console.WriteLine(a.PageInst.Number);

            //public fields
            Console.WriteLine("-----------");
            Console.WriteLine(a.Color);
            Console.WriteLine(a.PageInst.About);
            Console.WriteLine(a.PageInst.SetStr);
            

            Console.WriteLine(a.kek);
        }
    }
}
