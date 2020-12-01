using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectorLib
{
    public enum LifeTime
    {
        Singleton, /*на все запросы зависимостей возвращается один экземпляр объекта*/

        Instance   /*каждый новый запрос зависимости из контейнера приводит к созданию нового объекта*/
    }

    public class ImplementationInfo
    {
        internal Type ImplementationType;
        internal LifeTime lifeTime;

        public ImplementationInfo(Type type, LifeTime lf)
        {
            ImplementationType = type;
            lifeTime = lf;
        }
    }


}
