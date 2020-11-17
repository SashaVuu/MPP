using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TestGeneratorLib;

namespace TestsGeneratorLab
{
    class Program
    {
        
        static async Task Main(string[] args)
        {
            
            //TestCreator a = new TestCreator();
            //Console.WriteLine(FileData);

            //var tests = a.Generate(FileData);
            //foreach(TestStructure test in tests) 
            //{
            //    Console.WriteLine("--------------------------------------------------------------------------------------------------");
            //    Console.WriteLine("["+test.TestName+"]");
            //    Console.WriteLine( test.TestCode);
            //}

            string FolderPath = "C:\\BSUIR\\MPP\\res";

            List<string> FilesPath = new List<string>() {
                "C:\\BSUIR\\MPP\\files\\Faker.cs",
                "C:\\BSUIR\\MPP\\files\\Generator.cs",
                "C:\\BSUIR\\MPP\\files\\MultiplayerGame.cs"
            };

            Pipeline p = new Pipeline(new PipelineConfiguration(1,1,1));
            await p.Execute(FilesPath,FolderPath);
           
        }

    }
}
