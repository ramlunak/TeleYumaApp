using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using TeleYumaApp.Class;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.IO;
using System.Windows.Input;

namespace TeleYumaApp.ViewModels
{


    public class VMLlamar : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        Page CurrentPage => Application.Current.MainPage.Navigation?.NavigationStack?.LastOrDefault() ?? Application.Current.MainPage;

        public VMLlamar()
        {

        }

        private string _numero;

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; OnPropertyChanged(); }
        }

        public Command MyCommand { protected set; get; }

        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }

        private ICommand _LabelNumeroTappedCommand;
        public ICommand LabelNumeroTappedCommand
        {
            get
            {
                if (_LabelNumeroTappedCommand == null)
                {
                    _LabelNumeroTappedCommand = new RelayCommand(LabelNumeroTappedExecute, CanSubmitExecute);
                }
                return _LabelNumeroTappedCommand;
            }
        }
        public async void LabelNumeroTappedExecute(object parameter)
        {
            if (parameter is null) return;
            Numero = Numero + (string)parameter;
        }


        private ICommand _BorrarTappedCommand;
        public ICommand BorrarTappedCommand
        {
            get
            {
                if (_BorrarTappedCommand == null)
                {
                    _BorrarTappedCommand = new RelayCommand(BorrarTappedExecute, CanSubmitExecute);
                }
                return _BorrarTappedCommand;
            }
        }

        public async void BorrarTappedExecute(object parameter)
        {
            if (string.IsNullOrEmpty(Numero)) return;
            var lista = Numero.ToList();
            lista.RemoveAt(lista.Count-1);
            Numero = string.Empty;
            foreach (var item in lista)
            {
                Numero = Numero + item;
            }
           
        }

    }
}
