using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLab.TraceUtil
{
    public interface ITracer
    {

        public void StartTrace();
        public void StopTrace();
        public TraceResult GetTraceResult();

    }
}
