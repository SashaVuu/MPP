using FakerLib.Generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FakerLib.FakerUtil
{
    public class Generator
    {
       
        //Список всех генераторов
        private List<IValueGenerator> allGenerators = new List<IValueGenerator>();
        
        //Cписок плагинов
        private List<IValueGenerator> plugins = new List<IValueGenerator>();
        
        //Путь к папке с плагинами
        private string pluginsPath = "Plugins/";


        //Выгрузка плагинов из папки pluginPath в список plugins
        private void UploadPlugins(string pluginPath)
        {

            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
            if (!pluginDirectory.Exists)
                pluginDirectory.Create();

            //берем из директории все файлы с расширением .dll      
            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (var file in pluginFiles)
            {
                //загружаем сборку
                Assembly asm = Assembly.LoadFrom(file);

                //ищем типы, имплементирующие наш интерфейс IGenerator,
                //чтобы не захватить лишнего
                var types = asm.GetTypes().
                                Where(t => t.GetInterfaces().
                                Where(i => i.FullName == typeof(IValueGenerator).FullName).Any());

                //заполняем экземплярами полученных типов коллекцию плагинов
                foreach (var type in types)
                {
                    var plugin = asm.CreateInstance(type.FullName) as IValueGenerator;
                    plugins.Add(plugin);
                }
            }
        }

        public Generator() 
        {
            allGenerators.Add( new IntGenerator());
            allGenerators.Add( new FloatGenerator());
            allGenerators.Add( new CharGenerator());
            allGenerators.Add( new StringGenerator());
            allGenerators.Add( new ListGenerator());
            allGenerators.Add( new UriGenerator());
            UploadPlugins(pluginsPath);
            foreach (IValueGenerator plugin in plugins) 
            {
                allGenerators.Add(plugin);
            }

        }

        // Генерация значения. Если нет подходящего генератора, то генерируется дефолтное значение (0 или null)
        public object GenerateValue(GeneratorContext context) 
        {
            IValueGenerator generator = GetGenerator(context.TargetType);
            object value;

            if (generator!=null)
            {
                value = generator.Generate(context);
            }
            else
            {
                value = GetDefaultValue(context.TargetType);
                Console.WriteLine("Dont have generator for "+ context.TargetType.Name);
            }

            return value;
        }

        //Получаем генератор подходящего типа
        private IValueGenerator GetGenerator(Type valueType) 
        {
            IValueGenerator resultGenerator = null;
            foreach (IValueGenerator generator in allGenerators) 
            {
                if (generator.CanGenerate(valueType))
                {
                    resultGenerator = generator;
                }
            }
            return resultGenerator;
        }

        private object GetDefaultValue(Type t)
        {
            if (t.IsValueType)
                return Activator.CreateInstance(t);
            else
                return null;
        }

        public int GetAmountOfGenerators()
        {
            return allGenerators.Count;
        }
    }
}
