using System;
using System.Collections.Generic;
using System.Threading;
using TracerLab.Serializers;
using TracerLab.TraceUtil;
using TracerLab.Writer;

namespace TracerLab
{
    public class Program
    {
        static public Tracer tracer = new Tracer();

        static void Main(string[] args)
        {

            SomeMethod3();
            SomeMethod1();

            Thread myThread = new Thread(new ThreadStart(SomeMethod1));
            myThread.Start();
            
            ISerializer jsonSerializer = new JSONSerializer();
            ISerializer xmlSerializer = new XMLSerializer();

            IWriter consoleWriter = new ConsoleWriter();
            IWriter fileWriter = new FileWriter();

            consoleWriter.Write(xmlSerializer.Serialize(tracer.GetTraceResult()));
            fileWriter.Write(xmlSerializer.Serialize(tracer.GetTraceResult()));

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

        static public int Sum55()
        {
            int sum=0;
            for (int i=0;i<=10;i++)
            {
                sum += i;
            }
            return sum;
        }

        static public bool SomeMethod2()
        {
            tracer.StartTrace();
            int sum = 0;
            for (int i = 0; i <= 10; i++)
            {
                sum += i;
            }
            bool isIt = true;
            SomeMethod3();
            tracer.StopTrace();
            return isIt;

        }

        static public bool SomeMethod3()
        {
            tracer.StartTrace();
            int sum = 0;
            for (int i = 0; i <= 10; i++)
            {
                sum += i;
            }
            bool isIt = true;

            tracer.StopTrace();
            return isIt;

        }
    }
}
