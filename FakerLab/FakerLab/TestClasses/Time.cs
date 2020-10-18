using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLab.TestClasses
{
    public struct Time
    {

        public int hours, minutes, seconds;

        public Time(int hh, int mm, int ss)
        {
            hours = hh % 24;
            minutes = mm % 60;
            seconds = ss % 60;
        }

        public int Hours()
        {
            return hours;
        }  


    }
}
