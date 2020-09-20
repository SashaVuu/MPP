using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Xml.Serialization;
using TracerLab.TraceUtil;

namespace TracerLab.Serializers
{
    public class XMLSerializer:ISerializer
    {
        public void Serialize(TraceResult traceResult, string path)
        {
            try
            {
                XDocument xdoc = new XDocument();
                XElement threads = new XElement("root");
         
                foreach (KeyValuePair<int,ThreadSubstructure> currentThreadSubstructure in traceResult.threadSubstructures)
                {

                    XElement currentThreadElem = new XElement("thread");
                    XAttribute currentThreadId = new XAttribute("id", currentThreadSubstructure.Key);
                    XAttribute currentThreadTime = new XAttribute("time", currentThreadSubstructure.Value.threadTime);
                   
                    // Присоединение атрибутов к элементу.
                    currentThreadElem.Add(currentThreadId);
                    currentThreadElem.Add(currentThreadTime);

                    foreach (MethodSubstructure currentMethodSubstructure in currentThreadSubstructure.Value.listOfMethodSubstructures)
                    {
                        XElement currentMethodElem = CreateMethodElement(currentMethodSubstructure);
                        currentThreadElem.Add(currentMethodElem);
                    }

                    threads.Add(currentThreadElem);
                }

                xdoc.Add(threads);
                xdoc.Save(path);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private XElement CreateMethodElement (MethodSubstructure currentMethodSubstructure)
        {
                XElement currentMethodElem = new XElement("method");
                XAttribute currentMethodName= new XAttribute("class", currentMethodSubstructure.ClassOfMethod);
                XAttribute currentMethodClass = new XAttribute("name", currentMethodSubstructure.MethodName);
                XAttribute currentMethodTime = new XAttribute("time", currentMethodSubstructure.TraceTime.ToString());

                // Присоединение атрибутов к элементу.
                currentMethodElem.Add(currentMethodName);
                currentMethodElem.Add(currentMethodClass);
                currentMethodElem.Add(currentMethodTime);

                // Рекурсивный проход по вложенным методам.
                foreach (MethodSubstructure nestedMethod in currentMethodSubstructure.listOfMethodSubstructures) 
                {
                    currentMethodElem.Add(CreateMethodElement(nestedMethod));
                }

                return currentMethodElem;    
        }
    }
}
