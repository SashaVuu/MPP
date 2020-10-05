using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLab.Generators
{
    public class Generator
    {
        private Random rnd = new Random();

        private int upLimit = 256;
        private int downLimit = 0;

        public object GenerateInt() 
        {
            return rnd.Next(downLimit,upLimit);
        }

        public object GenerateFloat()
        {
            float value = (float)rnd.Next(downLimit, upLimit) / rnd.Next(downLimit, upLimit);
            return value;
        }

        public object GenerateLong()
        {
            return (long)(rnd.Next(downLimit, upLimit) / rnd.Next(downLimit, upLimit));
        }

        public object GenerateDouble()
        {
            double value = (double)rnd.Next(downLimit, upLimit) / rnd.Next(downLimit, upLimit);
            return value;
        }

        public object GenerateChar()
        {
            return (char)rnd.Next('A', 'Z');
        }

        public object GenerateString()
        {
            string currentString = "";
            for (int i=0;i<= rnd.Next(downLimit, upLimit);i++) 
            {
                currentString += (char)rnd.Next('A','Z');
            }
            return currentString;
        }

        public object GenerateList(Type objectsType)
        {
            return (char)rnd.Next('A', 'Z');
        }

        public object GenerateArray(Type objectsType)
        {
            return (char)rnd.Next('A', 'Z');
        }
    }
}
