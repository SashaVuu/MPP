using System;
using System.Collections.Generic;
using System.Text;

namespace DependencyInjectorTests
{

    public interface IGenericRepository
    {

    };

    public interface IGenericService
    {

    };

    public interface IGenericService<TGenericRepository> where TGenericRepository : IGenericRepository
    {

    };


    public class GenericRepositoryImpl : IGenericRepository
    {
        public GenericRepositoryImpl(int a) { }

    };

    public class GenericServiceImpl<TGenericRepository> : IGenericService<TGenericRepository> where TGenericRepository : IGenericRepository 
    {
        public TGenericRepository constructorCheck;
        public GenericServiceImpl (TGenericRepository a) 
        {
            constructorCheck = a;
        }

    };

}
