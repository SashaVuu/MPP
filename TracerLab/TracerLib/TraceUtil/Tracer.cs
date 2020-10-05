using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace TracerLib.TraceUtil
{
    public class Tracer : ITracer
    {
        private readonly TraceResult traceResult = new TraceResult();

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }

        public void StartTrace()
        {
            
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            StackFrame currentFrame = new StackTrace().GetFrame(2);
            MethodBase currentMethod = currentFrame.GetMethod();
            traceResult.StartTrace(new MethodSubstructure(currentMethod), currentThreadId);
        }

        public void StopTrace()
        {
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            traceResult.StopTrace(currentThreadId);
        }

    }
}
