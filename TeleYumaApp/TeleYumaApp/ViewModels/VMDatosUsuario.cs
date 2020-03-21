using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using TeleYumaApp.Class;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TeleYumaApp.ViewModels
{
   
    public class VMDatosUsuario : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Page CurrentPage => Application.Current.MainPage.Navigation?.NavigationStack?.LastOrDefault() ?? Application.Current.MainPage;


        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public VMDatosUsuario()
        {          
                   
        }
      
        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }

        #region NextCommand
        private ICommand _NextCommand;
        public ICommand NextCommand
        {
            get
            {
                if (_NextCommand == null)
                {
                    _NextCommand = new RelayCommand(NextExecute, CanSubmitExecute);
                }
                return _NextCommand;
            }
        }
           
        public async void NextExecute(object parameter)
        {
            ;
        }
        #endregion

        #region CancelarCommand
        private ICommand _CancelarCommand;
        public ICommand CancelarCommand
        {
            get
            {
                if (_CancelarCommand == null)
                {
                    _CancelarCommand = new RelayCommand(CancelarExecute, CanSubmitExecute);
                }
                return _CancelarCommand;
            }
        }

        public async void CancelarExecute(object parameter)
        {
            try
            {
                CurrentPage.Navigation.PopModalAsync();
            }
            catch (Exception ex)
            {

                ;
            }
        }
        #endregion

    }
}
