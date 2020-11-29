using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectorLib
{
    public class DependencyProvider
    {

        private DependencyConfig dependencyConfig;
        //Для разрешения циклических зависимостей
        private Stack<Type> cycleDepStack = new Stack<Type>();

        public DependencyProvider(DependencyConfig config)
        {
            dependencyConfig = config;
        }



        public object Resolve<TDependency>(object name = null)
        {

            return null;
        }


    }
}
