using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace TracerLab.TraceUtil
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
            //Узнаем какой метод вызвал данный, для инициализации полей  MethodSubstructure.
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            StackFrame currentFrame = new StackTrace().GetFrame(1);
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
