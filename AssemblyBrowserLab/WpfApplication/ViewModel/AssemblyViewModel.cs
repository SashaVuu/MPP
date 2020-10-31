using AssemblyBrowserLib.AssemblyStructureUtil;
using Microsoft.Win32;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WpfApplication.Command;

namespace WpfApplication.ViewModel
{
    class AssemblyViewModel : INotifyPropertyChanged
    {
     
        //Данные о сборке
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
        private RelayCommand _loadAssemblyCommand;
        public RelayCommand LoadAssemblyCommand
        {
            get
            {
                
                return _loadAssemblyCommand ??
                    (_loadAssemblyCommand = new RelayCommand(
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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    
    }
}
