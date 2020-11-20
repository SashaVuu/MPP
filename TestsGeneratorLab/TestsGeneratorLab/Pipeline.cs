using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using TestGeneratorLib;

namespace TestsGeneratorLab
{
    public class Pipeline
    {

        private readonly PipelineConfiguration pipelineConfig;
        public Pipeline(PipelineConfiguration PipelineConfig)
        {
            pipelineConfig = PipelineConfig;
        }


        public async Task Execute(List<string> filesPath,string outputPath)
        {
            
            //Блок чтения из файла
            //path,content
            var readingBlock = new TransformBlock<string, string>(
                async FilePath =>
                {
                    Console.WriteLine("File path:  "+ FilePath);
                    using (StreamReader reader = new StreamReader(FilePath))
                    {
                        return await reader.ReadToEndAsync();
                    }

                },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = pipelineConfig.MaxReadingTasks }
            );


            //Блок создания теста
            //code,tests
            var generateTestClass = new TransformManyBlock<string, TestStructure>(
                async Code =>
                {
                    Console.WriteLine("Generating tests... ");
                    return await TestCreator.Generate(Code);
                },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = pipelineConfig.MaxProcessingTasks }
            );


            //Блок записи теста в файл
            //TestStructure
            var writeGeneratedFile = new ActionBlock<TestStructure>(
                async testClass =>
                {
                    string fullpath = Path.Combine(outputPath, testClass.TestName);
                    Console.WriteLine("Fullpath "+ fullpath);
                    using (StreamWriter writer = new StreamWriter(fullpath))
                    {
                        await writer.WriteAsync(testClass.TestCode);
                    }

                },
                new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = pipelineConfig.MaxWritingTasks }
            );


            //Successful or unsuccessful completion of one block in the pipeline will
            //cause completion of the next block in the pipeline
            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };


            readingBlock.LinkTo(generateTestClass, linkOptions);
            generateTestClass.LinkTo(writeGeneratedFile, linkOptions);


            foreach (string path in filesPath) 
            {
                readingBlock.Post(path);
            }

            //Mark the head of the pipeline as complete.
            readingBlock.Complete();

            await writeGeneratedFile.Completion;

        }



    }
}
