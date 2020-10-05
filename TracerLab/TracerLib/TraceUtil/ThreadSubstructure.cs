using System;
using System.Collections.Generic;
using System.Reflection;
namespace TracerLib.TraceUtil
{
    [Serializable]
    public class ThreadSubstructure
    {
        public long threadTime;

        public List<MethodSubstructure> listOfMethodSubstructures = new List<MethodSubstructure>();

        [NonSerialized]
        public Stack<MethodSubstructure> stackOfMethodSubstructures = new Stack<MethodSubstructure>();
        
        public void StartTrace(MethodSubstructure method)
        {
            //Если на момент запуска трейсера стек пуст, то этот метод не является вложенным, добавление в список методов потока.
            if (stackOfMethodSubstructures.Count==0) 
            {
                listOfMethodSubstructures.Add(method);
            }
            stackOfMethodSubstructures.Push(method);
            method.StartTrace();
        }

        // *Если стек не пуст, то тот метод, который ниже по стеку является родителем(вложенным в) того, кто выше.
        public void StopTrace() 
        {
                MethodSubstructure currentMethod = stackOfMethodSubstructures.Pop();
                MethodSubstructure parentMethod = null;

                currentMethod.StopTrace();

                threadTime = GetThreadTime();

                // Добавление в список вложенных методов (кого?) метода "родителя" его вложенных методов.
                if (stackOfMethodSubstructures.Count != 0) 
                {
                    parentMethod = stackOfMethodSubstructures.Peek();
                    parentMethod.listOfMethodSubstructures.Add(currentMethod);
                }
   
        }

        // Возвращает время работы потока(сумма времени методов "верхнего уровня").
        private long GetThreadTime() 
        {
            long time = 0;
            foreach (MethodSubstructure topLevelMethod in listOfMethodSubstructures)
            {
                time += topLevelMethod.TraceTime;
            }
            return time;
        }
    }
}
