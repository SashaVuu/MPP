using System;
using System.Collections.Generic;
using System.Threading;
using TracerLab.Serializers;
using TracerLab.TraceUtil;

namespace TracerLab
{
    class Program
    {
        static public Tracer tracer = new Tracer();
        static public List<Thread> listOfThread = new List<Thread>();
        static void Main(string[] args)
        {
            //tracer.StartTrace();
            SomeMethod1();
            Thread myThread = new Thread(new ThreadStart(SomeMethod1));
            myThread.Start();
            //tracer.StopTrace();
            JSONSerializer a = new JSONSerializer();
            a.Serialize(tracer.GetTraceResult(), "C:/BSUIR/a.json");
        }
        
        static public void SomeMethod1()
        {
            tracer.StartTrace();
            int someNumber = 100;
            someNumber = someNumber + 5; 
            SomeMethod2();
            SomeMethod3();
            tracer.StopTrace();

        }

        static public bool SomeMethod2()
        {
            tracer.StartTrace();
            int someNumber = 100;
            bool isIt = true;
            SomeMethod3();
            tracer.StopTrace();
            return isIt;

        }

        static public bool SomeMethod3()
        {
            tracer.StartTrace();
            int someNumber = 100;
            bool isIt = true;

            tracer.StopTrace();
            return isIt;

        }
    }
}
