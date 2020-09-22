using System;
using System.Collections.Concurrent;


namespace TracerLib.TraceUtil
{
    [Serializable]
    public class TraceResult
    {

        public ConcurrentDictionary<int,ThreadSubstructure> threadSubstructures = new ConcurrentDictionary<int, ThreadSubstructure>();

        public void StartTrace(MethodSubstructure method, int threadID)
        {
            //Добавление в список "струкр потоков" новой или возврат уже существующей.Ключ - Ид Потока.
            ThreadSubstructure threadSub = threadSubstructures.GetOrAdd(threadID, new ThreadSubstructure());
            threadSub.StartTrace(method);
        }

        public void StopTrace(int currentThreadId)
        {

            ThreadSubstructure currentThread;
            // Изъятие из списка "структур потока" запрашеваемого по ID.
            if (threadSubstructures.TryGetValue(currentThreadId, out currentThread))
            {
                currentThread.StopTrace();
            }
            else
            {
                Console.WriteLine("Thread not found.");
            }
        }
    }
}
