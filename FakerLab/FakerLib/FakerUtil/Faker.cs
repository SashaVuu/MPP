
using FakerLib.Generators;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace FakerLib.FakerUtil
{
    public class Faker:IFaker
    {

        private Generator generator = new Generator();
        private Stack<Type> cycleStack = new Stack<Type>();
        private Random rand = new Random();
        

        public T Create<T>() 
        {
            return (T)Create(typeof(T));
        }
       

         internal object Create(Type objectType) 
         {         
           
            if (generator.GetGenerator(objectType)!=null)
            {
                return generator.GenerateValue(new GeneratorContext(rand, objectType, this));
            }

            else
            {
                // Если в стеке уже встрчался обьект с таким типом, то 
                // объект получает значение по умолчанию (0 или null)
                if (!cycleStack.Contains(objectType))
                {
                    cycleStack.Push(objectType);
                    object currentObject = CreateObject(objectType);
                    if (currentObject!=null)
                    {
                        FillFields(currentObject);
                        FillProperties(currentObject);
                    }
                    cycleStack.Pop();
                    return currentObject;
                }
                else
                {
                    return GetDefaultValue(objectType);
                }
            }

        }

        private object CreateObject(Type objectType) {

            object currentObject = GetDefaultValue(objectType);
            ConstructorInfo[] constructorsInfo = objectType.GetConstructors();
            
            if (constructorsInfo.Length > 0)
            {
                //Выбираем конструктор с max кол-вом праметров
                ConstructorInfo constructorInfo = ChooseConstructor(constructorsInfo);

                if (constructorInfo != null)
                {
                    //Параметры конструктора
                    ParameterInfo[] paramsInfo = constructorInfo.GetParameters();

                    Object[] constructorParams = new Object[paramsInfo.Length];

                    Type paramType;

                    for (int i = 0; i < constructorParams.Length; i++)
                    {
                        paramType = paramsInfo[i].ParameterType;

                        constructorParams[i] = Create(paramType);

                    }
                    try
                    {
                        currentObject = constructorInfo.Invoke(constructorParams);
                    }
                    catch
                    {
                        throw new Exception("Error: Invoke(constructorParams).");
                    }
                }
                else 
                {
                    currentObject = null;
                }

            }

            return currentObject;
        }

        /* При наличии нескольких конструкторов следует отдавать предпочтение конструктору
         * с большим числом параметров*/
        private ConstructorInfo ChooseConstructor(ConstructorInfo[] constructorsInfo) 
        {
            ConstructorInfo maxParamConstructor = null;
            if (constructorsInfo.Length != 0)
            {
                maxParamConstructor = constructorsInfo[0];
                //ищем консруткор с максимальным кол-вом параметров
                for (int i = 0; i < constructorsInfo.Length; i++)
                {
                    if (constructorsInfo[i].GetParameters().Length > maxParamConstructor.GetParameters().Length)
                    {
                        maxParamConstructor = constructorsInfo[i];
                    }
                }
            }
            return maxParamConstructor;
        }


        //Заполнение полей объекта
        private void FillFields(object currentObject)
        {

            FieldInfo[] fields = currentObject.GetType().GetFields();
            foreach (FieldInfo fieledInfo in fields)
            {
                if (fieledInfo.IsPublic)
                {
                    fieledInfo.SetValue(currentObject, Create(fieledInfo.FieldType));
                }
            }
        }

        //Заполнение свойств объекта
        private void FillProperties(object currentObject)
        {

            PropertyInfo[] properties = currentObject.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.CanWrite)
                {
                    propertyInfo.SetValue(currentObject, Create(propertyInfo.PropertyType));
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
