using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FakerLab.FakerLib
{
    public class Faker:IFaker
    {
        public Faker()
        {
        }

        public T Create<T>() // публичный метод для пользователя
        {
            return (T)Create(typeof(T));
        }

        // Процедура создания и инициализации объекта.
        private object Create(Type objectType) // метод для внутреннего использования
        {
            object currentObject=CreateObject(objectType);
            //FillPublicFields();
            //FillPublicProperties();
            return currentObject;

        }

        public object CreateObject(Type objectType) {

            object currentObject = GetDefaultValue(objectType);
            ConstructorInfo[] constructorsInfo = objectType.GetConstructors();

            if (constructorsInfo.Length > 0)
            {
                //Выбираем конструктор с max кол-вом праметров
                ConstructorInfo constructorInfo = ChooseConstructor(constructorsInfo);

                //Параметры конструктора
                ParameterInfo[] paramsInfo = constructorInfo.GetParameters();

                Object[] constructorParams = new Object[paramsInfo.Length];

                Type paramType;

                for (int i = 0; i < constructorParams.Length; i++)
                {
                    paramType = paramsInfo[i].ParameterType;

                    //Если параметр класс и не потомок System 
                    if (paramType.IsClass && !paramType.FullName.StartsWith("System."))
                    {
                        //рекурсивно создаем обьект
                        constructorParams[i]=Create(paramType);
                    }
                    else
                    {
                        //получаем дефолтное значение
                        constructorParams[i]=GetDefaultValue(paramType);
                    }
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

            return currentObject;
        }

        /* При наличии нескольких конструкторов следует отдавать предпочтение конструктору
         * с большим числом параметров, однако если при попытке его использования 
         * возникло исключение, следует пытаться использовать остальные*/

        private ConstructorInfo ChooseConstructor(ConstructorInfo[] constructorsInfo) 
        {
            ConstructorInfo maxParamConstructor = constructorsInfo[0];
            //ищем консруткор с максимальным кол-вом параметров
            for (int i=0; i<constructorsInfo.Length;i++)
            {
                if (constructorsInfo[i].GetParameters().Length > maxParamConstructor.GetParameters().Length) {
                    maxParamConstructor = constructorsInfo[i];
                }
            }
            return maxParamConstructor;
        }

        //Заполнение полей объекта
        public void FillFields(object currentObject)
        {
            FieldInfo[] fields = currentObject.GetType().GetFields(BindingFlags.Public);
            foreach (FieldInfo fieledInfo in fields)
            {
                fieledInfo.SetValue(currentObject, CreateObject(fieledInfo.FieldType));
            }
        }

        //Заполнение свойств объекта
        public void FillProperties(object currentObject)
        {
            PropertyInfo[] properties = currentObject.GetType().GetProperties(BindingFlags.Public);
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (propertyInfo.CanWrite && propertyInfo.SetMethod.IsPublic)
                {
                    propertyInfo.SetValue(currentObject, CreateObject(propertyInfo.PropertyType));
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
