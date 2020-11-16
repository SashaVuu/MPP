using System;
using System.IO;
using TestGeneratorLib;

namespace TestsGeneratorLab
{
    class Program
    {
        static string FilePath = "C:\\Users\\shell\\source\\repos\\FakerLab\\FakerLab\\FakerLib\\Faker.cs";
        static string ResPath = "C:\\BSUIR\\res";
        static string FileData;
        static void Main(string[] args)
        {
            FileData = File.ReadAllText(FilePath);
            TestGenerator a = new TestGenerator();
            Console.WriteLine(FileData);

            var tests = a.Generate(FileData);
            foreach(TestStructure test in tests) 
            {
                Console.WriteLine("--------------------------------------------------------------------------------------------------");
                Console.WriteLine("["+test.TestName+"]");
                Console.WriteLine( test.TestCode);
            }
           
        }

    }
}
