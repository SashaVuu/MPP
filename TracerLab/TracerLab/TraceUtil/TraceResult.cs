using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Reflection;
using System.Text;

namespace TracerLab.TraceUtil
{
    [Serializable]
    public class TraceResult
    {
        public ConcurrentDictionary<int,ThreadSubstructure> threadSubstructures = new ConcurrentDictionary<int, ThreadSubstructure>();

        
        public void StartTrace(MethodSubstructure method, int threadID)
        {
            ThreadSubstructure threadSubstracture = threadSubstructures.GetOrAdd(threadID, new ThreadSubstructure());
            threadSubstracture.StartTrace(method);
        }

        public void StopTrace(int currentThreadId)
        {
            ThreadSubstructure currentThread;
            if (threadSubstructures.ContainsKey(currentThreadId))
            {
                threadSubstructures.TryGetValue(currentThreadId, out currentThread);
                currentThread.StopTrace();
            }
        }
    }
}
