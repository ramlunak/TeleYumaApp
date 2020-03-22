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
using System.Threading.Tasks;

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
            Nombre = "Royber Arias Moreno";
            Email = "teleyumamasteremail@gmail.com";

            ShowEditName = false;
            ShowEditEmail= false;
           
        }

        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }

        #region Propiedades

        private string _nombre;

        public string Nombre
        {
            get { return _nombre; }
            set { _nombre = value; OnPropertyChanged(); }
        }

        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }


        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { _isLoading = value; OnPropertyChanged(); }
        }

        private bool _showEditName;

        public bool ShowEditName
        {
            get { return _showEditName; }
            set { _showEditName = value; OnPropertyChanged(); ShowLabelName = !value; }
        }

        private bool _showEditEmail;

        public bool ShowEditEmail
        {
            get { return _showEditEmail; }
            set { _showEditEmail = value; OnPropertyChanged(); ShowLabelEmail = !value; }
        }

        private bool _showLabelName;

        public bool ShowLabelName
        {
            get { return _showLabelName; }
            set { _showLabelName = value; OnPropertyChanged(); }
        }

        private bool _showLabelemail;

        public bool ShowLabelEmail
        {
            get { return _showLabelemail; }
            set { _showLabelemail = value; OnPropertyChanged(); }
        }

        #endregion

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

        #region ActualizarNameCommand
        private ICommand _ActualizarNameCommand;
        public ICommand ActualizarNameCommand
        {
            get
            {
                if (_ActualizarNameCommand == null)
                {
                    _ActualizarNameCommand = new RelayCommand(ActualizarNameExecute, CanSubmitExecute);
                }
                return _ActualizarNameCommand;
            }
        }

        public async void ActualizarNameExecute(object parameter)
        {
            IsLoading = true;
            await Task.Delay(3000);
            IsLoading = false;

            ShowEditName = false;
          
        }

        #endregion

        #region ActualizarEmailCommand
        private ICommand _ActualizarEmailCommand;
        public ICommand ActualizarEmailCommand
        {
            get
            {
                if (_ActualizarEmailCommand == null)
                {
                    _ActualizarEmailCommand = new RelayCommand(ActualizarEmailExecute, CanSubmitExecute);
                }
                return _ActualizarEmailCommand;
            }
        }

        public async void ActualizarEmailExecute(object parameter)
        {
            IsLoading = true;
            await Task.Delay(3000);
            IsLoading = false;

            ShowEditEmail = false;
        }

        #endregion

        #region ShowEditNameCommand
        private ICommand _ShowEditNameCommand;
        public ICommand ShowEditNameCommand
        {
            get
            {
                if (_ShowEditNameCommand == null)
                {
                    _ShowEditNameCommand = new RelayCommand(ShowEditNameExecute, CanSubmitExecute);
                }
                return _ShowEditNameCommand;
            }
        }

        public async void ShowEditNameExecute(object parameter)
        {
            ShowEditName = true;
        }

        #endregion

        #region ShowEditEmailCommand
        private ICommand _ShowEditEmailCommand;
        public ICommand ShowEditEmailCommand
        {
            get
            {
                if (_ShowEditEmailCommand == null)
                {
                    _ShowEditEmailCommand = new RelayCommand(ShowEditEmailExecute, CanSubmitExecute);
                }
                return _ShowEditEmailCommand;
            }
        }

        public async void ShowEditEmailExecute(object parameter)
        {
            ShowEditEmail = true;
        }

        #endregion

    }
}
