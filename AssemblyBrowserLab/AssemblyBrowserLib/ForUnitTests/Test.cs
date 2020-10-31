using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace AssemblyBrowserLib.ForUnitTests
{
    public class Test
    {
        //Fields
        private int privateIntField;

        public bool publicBoolField;

        protected string protectedStringField;

        //Properties
        public int publicIntProperty { get; set; }
        
        private string name;
        public string Name
        {
            get
            {
                return name;
            }

            private set
            {
                name = value;
            }
        }


        //Methods
        private int privateIntMethod( int a, bool b ) { return 1; }
        internal void internalVoidMethod() { }

        static void staticVoidMethod() { }



    }


}
