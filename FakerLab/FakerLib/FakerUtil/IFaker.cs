using System;


namespace FakerLib.FakerUtil
{
    public interface IFaker
    {
        T Create<T>();

        object Create(Type type);
    }
}
