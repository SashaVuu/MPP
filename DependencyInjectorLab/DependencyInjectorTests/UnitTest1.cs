using DependencyInjectorLib;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DependencyInjectorTests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void SimpleDependencyTest()
        {
            DependencyConfig dependencies = new DependencyConfig();
            dependencies.Register<IService, ServiceImpl1>(LifeTime.Singleton);

            DependencyProvider provider = new DependencyProvider(dependencies);
            var service1 = provider.Resolve<IService>();
            Assert.IsNotNull(service1);

        }


        [Test]
        public void SingletonTest()
        {
            DependencyConfig dependencies = new DependencyConfig();
            dependencies.Register<IService, ServiceImpl1>(LifeTime.Singleton);

            DependencyProvider provider = new DependencyProvider(dependencies);
            var service1 = provider.Resolve<IService>();
            var service2 = provider.Resolve<IService>();
            Assert.IsNotNull(service1);
            Assert.AreEqual(service1, service2);
        }


        //IEnumerable
        [Test]
        public void ManyImplementationTest()
        {
            DependencyConfig dependencies = new DependencyConfig();
            dependencies.Register<IService, ServiceImpl1>(LifeTime.Instance);
            dependencies.Register<IService, ServiceImpl2>(LifeTime.Instance);

            DependencyProvider provider = new DependencyProvider(dependencies);
            var services = provider.Resolve<IEnumerable<IService>>() as Array;

            Assert.AreEqual(services.Length, 2);
            Assert.AreEqual(services.GetValue(0).GetType(), typeof(ServiceImpl1));
            Assert.AreEqual(services.GetValue(1).GetType(), typeof(ServiceImpl2));

        }


        //IHuman->IAnimal->IHuman
        [Test]
        public void CycleDependecies()
        {
            DependencyConfig dependencies = new DependencyConfig();
            dependencies.Register<IHuman, Woman>(LifeTime.Instance);
            dependencies.Register<IAnimal, Cat>(LifeTime.Instance);

            DependencyProvider provider = new DependencyProvider(dependencies);
            Woman service = (Woman)provider.Resolve<IHuman>();

            //IHuman->IAnimal (NOT NULL)
            Assert.IsNotNull(service.getValue());

            //IHuman->IAnimal->IHuman (NULL)
            Assert.IsNull(service.myCat.getValue());

        }


        //IService<IRepository>
        [Test]
        public void GenericDependenciesTest()
        {
            DependencyConfig dependencies = new DependencyConfig();
            dependencies.Register<IRepository, RepositoryImpl>(LifeTime.Instance);
            dependencies.Register<IService<IRepository>, ServiceImpl<IRepository>>(LifeTime.Instance);

            DependencyProvider provider = new DependencyProvider(dependencies);
            var service = (ServiceImpl<IRepository>)provider.Resolve<IService<IRepository>>();

            Assert.IsNotNull(service.checkConstructor);
            Assert.AreEqual(service.checkConstructor.GetType(), typeof(RepositoryImpl));
            Assert.AreEqual(service.GetType(), typeof(ServiceImpl<IRepository>));
        }


        //GenericServiceImpl<IGenericRepository>
        [Test]
        public void OpenGenericDependenciesTest()
        {
            DependencyConfig dependencies = new DependencyConfig();
            dependencies.Register(typeof(IGenericService<>), typeof(GenericServiceImpl<>), LifeTime.Instance);
            dependencies.Register<IGenericRepository, GenericRepositoryImpl>(LifeTime.Instance);

            DependencyProvider provider = new DependencyProvider(dependencies);
            GenericServiceImpl<IGenericRepository> service = (GenericServiceImpl<IGenericRepository>)provider.Resolve<IGenericService<IGenericRepository>>();

            Assert.AreEqual(service.GetType(), typeof(GenericServiceImpl<IGenericRepository>));
            Assert.IsNotNull(service.constructorCheck);
        }

        [Test]
        public void ConfigExceptionTests()
        {
            DependencyConfig dependencies = new DependencyConfig();

            dependencies.Register<IRepository, RepositoryImpl>(LifeTime.Instance);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => dependencies.Register<IRepository, RepositoryImpl>(LifeTime.Instance));
            Assert.AreEqual("Such dependency is already registered", ex.Message);

            ArgumentException ex2 = Assert.Throws<ArgumentException>(() => dependencies.Register(typeof(IRepository), typeof(ServiceImpl1), LifeTime.Instance));
            Assert.AreEqual("TImplementation doesn't implement TDependency interface", ex2.Message);

            ArgumentException ex3 = Assert.Throws<ArgumentException>(() => dependencies.Register(typeof(IService), typeof(AbstractClass), LifeTime.Instance));
            Assert.AreEqual("TImplementation can't be an abstract class", ex3.Message);

            ArgumentException ex4 = Assert.Throws<ArgumentException>(() => dependencies.Register(typeof(IService), typeof(NonConstructor), LifeTime.Instance));
            Assert.AreEqual("TImplementation doesn't have any public constructors", ex4.Message);

        }

        [Test]
        public void ProviderExceptionTests()
        {
            DependencyConfig dependencies = new DependencyConfig();

            //Не добавлена зависимость
            DependencyProvider provider = new DependencyProvider(dependencies);
            KeyNotFoundException ex = Assert.Throws<KeyNotFoundException>(() => provider.Resolve<IService>());

            //Не добавлена зависимость,случай множества имплементаций
            KeyNotFoundException ex2 = Assert.Throws<KeyNotFoundException>(() => provider.Resolve<IEnumerable<IService>>());

            //Не добавлена зависимость, Generic 

            //dependencies.Register<IRepository, RepositoryImpl>(LifeTime.Instance);
            dependencies.Register<IService<IRepository>, ServiceImpl<IRepository>>(LifeTime.Instance);  
            
            KeyNotFoundException ex3 = Assert.Throws<KeyNotFoundException>(() => provider.Resolve<IService<IRepository>>());

            //Не добавлена зависимость,случай OpenGeneric

            //dependencies.Register(typeof(IGenericService<>), typeof(GenericServiceImpl<>), LifeTime.Instance);
            //dependencies.Register<IGenericRepository, GenericRepositoryImpl>(LifeTime.Instance);

            KeyNotFoundException ex4 = Assert.Throws<KeyNotFoundException>(() => provider.Resolve<IGenericService<IGenericRepository>>());

        }
    }
}