using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyBrowserLib.ForUnitTests
{
    public class Test
    {
        private int privateIntField;
        public bool publicBoolField;
        protected string protectedStringProperty {  get; set; }

        private int privateIntMethod() { return 1; }

    }


}
