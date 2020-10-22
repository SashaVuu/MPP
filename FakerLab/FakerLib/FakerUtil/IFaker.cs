using System;


namespace FakerLib.FakerUtil
{
    public interface IFaker
    {
        T Create<T>();

    }
}
