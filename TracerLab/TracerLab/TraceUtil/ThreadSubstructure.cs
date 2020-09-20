using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TracerLab.TraceUtil
{
    [Serializable]
    public class ThreadSubstructure
    {
        public long threadTime;

        public List<MethodSubstructure> listOfMethodSubstructures = new List<MethodSubstructure>();

        [NonSerialized]
        public Stack<MethodSubstructure> stackOfMethodSubstructures = new Stack<MethodSubstructure>();

        public ThreadSubstructure() 
        {
            stackOfMethodSubstructures.Push(null);
        }

        
        public void StartTrace(MethodSubstructure method)
        {
            if (stackOfMethodSubstructures.Count==1) 
            {
                listOfMethodSubstructures.Add(method);
            }
            stackOfMethodSubstructures.Push(method);
            method.StartTrace();
        }

        public void StopTrace() 
        {
            if (stackOfMethodSubstructures.Count!=0)
            {
                MethodSubstructure currentMethod = stackOfMethodSubstructures.Pop();
                MethodSubstructure parentMethod = stackOfMethodSubstructures.Peek();
                currentMethod.StopTrace();
                threadTime += currentMethod.TraceTime;
                if (parentMethod != null) {
                    parentMethod.listOfMethodSubstructures.Add(currentMethod);
                }
            }
        }
    }
}
