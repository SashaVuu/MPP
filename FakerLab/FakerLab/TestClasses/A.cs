using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLab.TestClasses
{
    public class A
    {
        public int aInt {  set; get; }
        public List<List<Page>> aListListPage;
        public A instA;
        public double aDouble;
        private int aIntPrvt;

        public A(List<List<Page>> a)
        {
            aListListPage = a;
        }
    }
}
