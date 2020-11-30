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
        }

       


        //IEnumerable
        [Test]
        public void IEnumerableTest()
        {
            DependencyConfig dependencies = new DependencyConfig();
            dependencies.Register<IService, ServiceImpl1>(LifeTime.Instance);
            dependencies.Register<IService, ServiceImpl2>(LifeTime.Instance);

            DependencyProvider provider = new DependencyProvider(dependencies);
            var services = provider.Resolve<IEnumerable<IService>>() as Array;

            Assert.AreEqual(services.Length, 2);
            Assert.AreEqual(services.GetValue(0).GetType(),typeof(ServiceImpl1));
            Assert.AreEqual(services.GetValue(1).GetType(),typeof(ServiceImpl2));

        }


        //IHuman->IAnimal->IHuman
        [Test]
        public void CycleDependecies()
        {
            DependencyConfig dependencies = new DependencyConfig();
            dependencies.Register<IHuman,Woman>(LifeTime.Instance);
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
            Assert.AreEqual(service.GetType(),typeof(ServiceImpl<IRepository>));
        }


        //GenericServiceImpl<IGenericRepository>
        [Test]
        public void OpenGenericDependenciesTest()
        {
            DependencyConfig dependencies = new DependencyConfig();
            dependencies.Register(typeof(IGenericService<>), typeof(GenericServiceImpl<>),LifeTime.Instance);
            dependencies.Register<IGenericRepository, GenericRepositoryImpl>(LifeTime.Instance);

            DependencyProvider provider = new DependencyProvider(dependencies);
            GenericServiceImpl<IGenericRepository> service = (GenericServiceImpl<IGenericRepository>)provider.Resolve<IGenericService<IGenericRepository>>();
            
            //??????????
            Assert.AreEqual(service.GetType(),typeof(GenericServiceImpl<IGenericRepository>));
            Assert.IsNotNull(service.constructorCheck);
        }


    }
}