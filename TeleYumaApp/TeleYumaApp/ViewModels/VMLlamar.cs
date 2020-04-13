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

        public rlkControles.entry TxtTelefono { get; set; }

        public VMLlamar(ref rlkControles.entry txtTelefono)
        {
            TxtTelefono = txtTelefono;
        }

        private string _numero;

        public string Numero
        {
            get { return _numero; }
            set { _numero = value; OnPropertyChanged(); }
        }

        private int _CursorPosition;

        public int CursorPosition
        {
            get { return _CursorPosition; }
            set { _CursorPosition = value; OnPropertyChanged(); }
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
            try
            {
                if (parameter is null) return;
                if (string.IsNullOrEmpty(Numero))
                {
                    Numero = Numero + (string)parameter;
                    return;
                }

                var lista = Numero.ToList();
                if (CursorPosition != 0 && Numero.Length > 1)
                {
                    var i = 0;
                    var text = string.Empty;
                    foreach (var item in lista)
                    {
                        if (CursorPosition == i)
                            text = text + (string)parameter + item;
                        else
                            text = text + item;
                        i++;
                    }
                    Numero = text;
                }

                else
                    Numero = Numero + (string)parameter;
            }
            catch (Exception)
            {

                Numero = Numero + (string)parameter;
            }
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


        public static int Position { get; set; }

        public async void BorrarTappedExecute(object parameter)
        {
            try
            {
                if (string.IsNullOrEmpty(Numero)) return;

                var lista = Numero.ToList();
                Position = CursorPosition;
                if (Position == 0)
                    Position = lista.Count;
                lista.RemoveAt(Position - 1);
                Numero = string.Empty;
                var text = string.Empty;
                foreach (var item in lista)
                {
                    text = text + item;
                }
                Numero = text;
                TxtTelefono.Focus();
                CursorPosition = Position;
                TxtTelefono.Focus();
            }
            catch (Exception ex)
            {

                ;
            }
        }

    }
}
