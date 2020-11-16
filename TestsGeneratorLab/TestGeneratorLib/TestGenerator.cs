using System.Collections.Generic;


namespace TestGeneratorLib
{
    public class TestGenerator
    {
        
        public List<TestStructure> Generate(string content) 
        {
            TestCreator tc = new TestCreator();

            var result=tc.GetTestContent(content);

            return result;
        }


    }
}
