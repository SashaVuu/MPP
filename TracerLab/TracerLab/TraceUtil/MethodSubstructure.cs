using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace TracerLab.TraceUtil
{
    [Serializable]
    // Содержит основную информацию о методе и его "потомках"
    public class MethodSubstructure
    {
        public string MethodName;
        public string ClassOfMethod;
        public long TraceTime;
        public List<MethodSubstructure> listOfMethodSubstructures = new List<MethodSubstructure>();

        private Stopwatch stopWatch = new Stopwatch();

        public MethodSubstructure() { }
        public MethodSubstructure(MethodBase method)
        {
            MethodName = method.Name;
            ClassOfMethod = method.DeclaringType.Name;
            TraceTime = 0;
        }
        
        public void StartTrace()
        {
            stopWatch.Start();
        }

        public void StopTrace()
        {
            stopWatch.Stop();
            TraceTime = stopWatch.ElapsedTicks;
        }
        
    }
}
