using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectorTests
{
    public interface IAnimal 
    {
        public IHuman getValue();

    }

    public class Cat :IAnimal
    {
        public IHuman myPreviousOwner;
        public Cat(IHuman previousOwner)
        {
           myPreviousOwner = previousOwner;
        }
        public IHuman getValue()
        {
            return myPreviousOwner;
        }

    }

    public interface IHuman
    {
        public IAnimal getValue();
    }

    public class Woman : IHuman
    {
        public IAnimal myCat;
        public Woman(IAnimal cat)
        {
            myCat = cat;
        }
        public IAnimal getValue()
        {
            return myCat;
        }
    }

  

}
