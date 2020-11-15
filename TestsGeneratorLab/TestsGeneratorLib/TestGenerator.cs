using System;
using System.Collections.Generic;
using System.Text;
using TestsGeneratorLib.CodeStructUtil;

namespace TestsGeneratorLib
{
    public class TestGenerator
    {
        
        public string Generate(string content) 
        {
            //Создаем структуру исходного кода
            CodeStructure cs = new CodeStructure(content);

            //Используя созданную структуру, формируем тесты
            TestCreator tc = new TestCreator(cs);

            string result=tc.GetTestContent();
            return result;
        }


    }
}
