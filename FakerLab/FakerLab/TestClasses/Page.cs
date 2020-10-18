using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLab.TestClasses
{
    public class Page
    {
        public int Number;
        public string About;
        public Book MyBook;
        public string SetStr { set; get; }

        public Page(int number) {
            Number = number;
        }
        public string Method()
        {
            string hi = "hello";
            string wrld = "world";

            return hi + " " + wrld;

        }

    }
}
