using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DependencyInjectorLib
{
    public class DependencyProvider
    {

        private DependencyConfig dependencyConfig;

        //Для разрешения циклических зависимостей
        private Stack<Type> cycleStack = new Stack<Type>();

        //Singleton
        private ConcurrentDictionary<Type, object> ImplementationInstances = new ConcurrentDictionary<Type, object>();


        public DependencyProvider(DependencyConfig config)
        {
            dependencyConfig = config;
        }


        public TDependency Resolve<TDependency>()
        {
            return (TDependency)Resolve(typeof(TDependency));
        }

        public object Resolve(Type DependencyType)
        {
            object resultObj = null;
            // Если в стеке уже встречался обьект с таким типом, то 
            // объект получает значение по умолчанию (0 или null)
            if (!cycleStack.Contains(DependencyType))
            {
                cycleStack.Push(DependencyType);
                List<ImplementationInfo> implInfos;


                // 1.IEnumerable<IService> services = provider.Resolve<IEnumerable<IService>>();
                if (typeof(IEnumerable).IsAssignableFrom(DependencyType)) 
                {
                    Type actualDependency = DependencyType.GetGenericArguments()[0];
                    if (dependencyConfig.RegisteredDependencies.ContainsKey(actualDependency)) {
                        implInfos = dependencyConfig.RegisteredDependencies[actualDependency];
                        int implCount = implInfos.Count;
                        var array = Array.CreateInstance(actualDependency, implCount);

                        for (int i = 0; i < implCount; i++)
                            array.SetValue(GetObject(implInfos[i]), i);

                        cycleStack.Pop();
                        return array;
                    }
                    else
                    {
                        throw new KeyNotFoundException($"Dependency {actualDependency.Name} is not registered.");
                    }

                }

                // 2.Open generic dependency
                //Если это дженерик и имплементация IService<> есть в словаре 
                if (DependencyType.IsGenericType && dependencyConfig.RegisteredDependencies.ContainsKey(DependencyType.GetGenericTypeDefinition()))
                {
                    //Получаем из IService<IRepos>  -> IService'
                    Type GenericType = DependencyType.GetGenericTypeDefinition();
                    
                    //Вытягиваем реализацию ServiceImpl
                    ImplementationInfo implInfo = dependencyConfig.RegisteredDependencies[GenericType].First();

                    //ServiceImpl<>  -> ServiceImpl<IRepos>
                    implInfo.ImplementationType = implInfo.ImplementationType.MakeGenericType(DependencyType.GetGenericArguments()[0]);

                    object result = GetObject(implInfo);
                    cycleStack.Pop();

                    return result;
                    
                }


                // 3.Simple Generic Dependency or SimpleDependency
                if (dependencyConfig.RegisteredDependencies.ContainsKey(DependencyType))
                {
                    implInfos = dependencyConfig.RegisteredDependencies[DependencyType];
                    
                    object result = GetObject(implInfos.First());
                    cycleStack.Pop();
                    
                    return result;
                }
                else if (!dependencyConfig.RegisteredDependencies.ContainsKey(DependencyType))
                {
                    throw new KeyNotFoundException($"Dependency {DependencyType.Name} is not registered.");
                }

            }
            else
            {
                return GetDefaultValue(DependencyType);
            }

            return resultObj;
        }


        //Выбор действий в зависимости от Singleton или Instance
        private object GetObject(ImplementationInfo implInfo)
        {
            if (implInfo.lifeTime==LifeTime.Singleton)
            {
                if (!ImplementationInstances.ContainsKey(implInfo.ImplementationType))
                {
                    var impl = CreateObject(implInfo.ImplementationType);
                    ImplementationInstances.TryAdd(implInfo.ImplementationType, impl);
                }
                //Возврат значения из словаря
                return ImplementationInstances[implInfo.ImplementationType];
            }
            else
            {
                //Создание нового объекта
                return CreateObject(implInfo.ImplementationType);
            }
        }

        //Создание объекта
        private object CreateObject(Type t)
        {
            ConstructorInfo[] constructors = t.GetConstructors();

            if (constructors.Length == 0)
            {
                throw new InvalidOperationException($"No public constructors for {t.Name}.");
            }
            else
            {
                ConstructorInfo constructor = constructors.OrderBy(x => x.GetParameters().Length).Last();
                ParameterInfo[] parameters = constructor.GetParameters();
                Object[] constructorParams = new Object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    Type paramType = parameters[i].ParameterType;

                    //Если параметр - зависимость, то рекурсивно создаем
                    if (dependencyConfig.RegisteredDependencies.ContainsKey(paramType))
                    {
                        object paramObj = Resolve(paramType);
                        constructorParams[i] = paramObj;
                    }
                    else if (paramType.Namespace == "System")
                    {
                        constructorParams[i] = GetDefaultValue(paramType);
                    }
                    else 
                    {
                        throw new KeyNotFoundException($"Dependency {paramType.Name} is not registered.");
                    }

                }

                //Создаем объект
                try
                {
                    var instance = constructor.Invoke(constructorParams);
                    return instance;
                }
                catch (Exception)
                {
                    throw new Exception($"Problem with invoking the instance.");
                }

            }

        }

        private object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                // Для типов-значений вызов конструктора по умолчанию даст default(T).
                return Activator.CreateInstance(t);
            else
                // Для ссылочных типов значение по умолчанию всегда null.
                return null;
        }


    }
}
