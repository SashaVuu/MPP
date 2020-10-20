using AssemblyBrowserLib.AssemblyStructureUtil;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfAssemblyBrowserApp.Command;

namespace WpfAssemblyBrowserApp.ViewModel
{
    class AssemblyViewModel : INotifyPropertyChanged
    {
     
        //тут данные о сборке
        private List<AssemblyNamespace> _assemblyNamespaces;
        public List<AssemblyNamespace> AssemblyNamespaces
        {
            get
            {
                return _assemblyNamespaces;
            }
            set
            {
                _assemblyNamespaces = value;
                OnPropertyChanged("AssemblyNamespaces");
            }
        }

        // Команда загрузки сборки

        /* Команда хранится в свойстве AddCommand и представляет собой объект выше
         * определенного класса RelayCommand. Этот объект в конструкторе принимает 
         * действие - делегат Action<object>. 
         * Здесь действие представлено в виде лямбда-выражения, 
         * которое добавляет в коллекцию Phones новый объект Phone и устанавливает
         * его в качестве выбранного.*/

        private RelayCommand _loadAssemblyCommand;
        public RelayCommand LoadAssemblyCommand
        {
            get
            {
                
                return _loadAssemblyCommand ??
                    (new RelayCommand(
                        obj =>
                        {
                            OpenFileDialog openFileDialog = new OpenFileDialog();
                            openFileDialog.Filter = "Assembly |*.dll";
                            if (openFileDialog.ShowDialog() == true)
                            {
                                AssemblyInfo.LoadAssemblyByPath(openFileDialog.FileName);
                                AssemblyNamespaces = AssemblyInfo.assemblyStructure.nameSpaces;
                            }
                        }
                    ));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            //Проверка что кто-то подписался на это событие,если да то передаем аргументы (т.е. имя свойства)
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    
    }
}
