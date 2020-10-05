using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLab.TestClasses
{
    public class Book
    {
        public readonly string Name;
        public readonly int Price;
        public A Aw;
        public Book(string name, int price, A kek)
        {
            Name = name;
            Price = price;
            Aw = kek;
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
