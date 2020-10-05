using System;
using System.Collections.Generic;
using System.Text;

namespace FakerLab.FakerLib
{
    public interface IFaker
    {
        public T Create<T>();
    }
}
