using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLab.TestClasses
{
    public class Book
    {
        public readonly string Name;
        public readonly int Price;
        public Page PageInst;
        public int Color;
        public List<int> list;
        public double kek;
        public Time time;

        public Book(string name, int price, Page kek)
        {
            Name = name;
            Price = price;
            PageInst = kek;
        }

        public Book(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public void Show()
        {
            Console.WriteLine(Name + " " + Price);
        }

    }
}
