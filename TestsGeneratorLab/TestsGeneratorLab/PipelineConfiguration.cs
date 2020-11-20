using System;
using System.Collections.Generic;
using System.Text;

namespace TestsGeneratorLab
{
    public class PipelineConfiguration
    {
        //Ограничение количества файлов, загружаемых за раз
        public int MaxReadingTasks { get; }


        //Ограничение максимального количества одновременно обрабатываемых задач
        public int MaxProcessingTasks { get; }


        //Ограничение количества одновременно записываемых файлов
        public int MaxWritingTasks { get; }


        public PipelineConfiguration(int maxReadingTasks, int maxProcessingTasks, int maxWritingTasks)
        {
            MaxReadingTasks = maxReadingTasks;
            MaxProcessingTasks = maxProcessingTasks;
            MaxWritingTasks = maxWritingTasks;
        }
    }
}
