using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using TeleYumaApp.Class;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using System.Linq;
using TeleYumaApp.Teleyuma;

namespace TeleYumaApp.ViewModels
{

    public class VMRecargas2 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public VMRecargas2()
        {

            MostrarPais();

            ImgPais = "phone_black";

            Recargas = new ObservableCollection<SQ_Recarga>(_Global.Recargas);
            Productos = new ObservableCollection<Producto>();
        }


        #region PROIEDADES

        ObservableCollection<SQ_Recarga> _Recargas { get; set; }
        public ObservableCollection<SQ_Recarga> Recargas { get { return _Recargas; } set { _Recargas = value; OnPropertyChanged(); } }

        ObservableCollection<Producto> _Productos { get; set; }
        public ObservableCollection<Producto> Productos { get { return _Productos; } set { _Productos = value; OnPropertyChanged(); } }


        public ICollection<string> Paises
        {
            get => Pais.Paises.ToList();

        }

        public ICollection<string> Montos
        {
            get => new List<string> {
                "$10.00",
                "$15.00",
                "$20.00",
                "$25.00",
                "$30.00",
                "$35.00",
                "$40.00",
                "$45.00",
                "$50.00"
            };

        }


        private DingProducto _TipoProducto;
        public DingProducto TipoProducto
        {
            get { return _TipoProducto; }
            set { _TipoProducto = value; }
        }


        private bool _ShowRegionPais;
        public bool ShowRegionPais
        {
            get { return _ShowRegionPais; }
            set { _ShowRegionPais = value; OnPropertyChanged(); }
        }

        private bool _ShowRegionProductos;
        public bool ShowRegionProductos
        {
            get { return _ShowRegionProductos; }
            set { _ShowRegionProductos = value; OnPropertyChanged(); }
        }

        private bool _ShowRegionTelefono;
        public bool ShowRegionTelefono
        {
            get { return _ShowRegionTelefono; }
            set { _ShowRegionTelefono = value; OnPropertyChanged(); }
        }

        private string _txtPais;
        public string txtPais
        {
            get { return _txtPais; }
            set { _txtPais = value; OnPropertyChanged(); }
        }

        private bool _txtNumeroFocus;
        public bool txtNumeroFocus
        {
            get { return _txtNumeroFocus; }
            set { _txtNumeroFocus = value; OnPropertyChanged(); }
        }


        private string _RegexValidate;
        public string RegexValidate
        {
            get { return "^0?53([0-9]{8})$"; }
            set { _RegexValidate = value; OnPropertyChanged(); }
        }


        private string _txtNumero;
        public string txtNumero
        {
            get { return _txtNumero; }
            set { _txtNumero = value; OnPropertyChanged(); }
        }

        private string _Prefijo;
        public string Prefijo
        {
            get { return _Prefijo; }
            set { _Prefijo = value; OnPropertyChanged(); }
        }

        private bool _ShowPrefijo;
        public bool ShowPrefijo
        {
            get { return _ShowPrefijo; }
            set { _ShowPrefijo = value; OnPropertyChanged(); }
        }

        private bool _ShowSim;
        public bool ShowSim
        {
            get { return _ShowSim; }
            set { _ShowSim = value; OnPropertyChanged(); }
        }

        private bool _ShowTipoRecarga;
        public bool ShowTipoRecarga
        {
            get { return _ShowTipoRecarga; }
            set { _ShowTipoRecarga = value; OnPropertyChanged(); }
        }

        private bool _ShowRecargaMovil;
        public bool ShowRecargaMovil
        {
            get { return _ShowRecargaMovil; }
            set { _ShowRecargaMovil = value; OnPropertyChanged(); }
        }

        private bool _ShowRecargaNauta;
        public bool ShowRecargaNauta
        {
            get { return _ShowRecargaNauta; }
            set { _ShowRecargaNauta = value; OnPropertyChanged(); }
        }

        private bool _ShowButton;
        public bool ShowButton
        {
            get { return _ShowButton; }
            set { _ShowButton = value; OnPropertyChanged(); }
        }

        private bool _RadioMovilCheked;
        public bool RadioMovilCheked
        {
            get { return _RadioMovilCheked; }
            set { _RadioMovilCheked = value; OnPropertyChanged(); }
        }

        private bool _RadioNautaCheked;
        public bool RadioNautaCheked
        {
            get { return _RadioNautaCheked; }
            set { _RadioNautaCheked = value; OnPropertyChanged(); }
        }

        private string _ImgPais;
        public string ImgPais
        {
            get { return _ImgPais; }
            set { _ImgPais = value; OnPropertyChanged(); }
        }

        #endregion

        #region METODOS PUBLICOS
        public void CargarFotoPais(string nombre)
        {
            try
            {
                var pais = Pais.GetList().First(p => p.Nombre == nombre);
                if (pais != null)
                {
                    ImgPais = pais.image;
                    ShowSim = false;
                    Prefijo = pais.PrefijoTelefonico;
                    ShowPrefijo = true;
                }
            }
            catch
            {


            }
        }

        public void MostrarPais()
        {
            ShowRegionPais = true;
            ShowRegionProductos = false;
            ShowRegionTelefono = false;
        }

        public void MostrarProductos()
        {
            ShowRegionPais = false;
            ShowRegionProductos = true;
            ShowRegionTelefono = false;
        }

        public void MostrarNumero()
        {
            ShowRegionPais = false;
            ShowRegionProductos = false;
            ShowRegionTelefono = true;
        }

        public void ResetFotoPais()
        {
            ImgPais = "place";
            ShowSim = true;
            Prefijo = "";
            ShowPrefijo = false;
        }

        public void ResetFormsMovil()
        {
            ResetFotoPais();
            txtPais = string.Empty;
            txtNumero = string.Empty;
        }

        public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } = (text, values) => values
          .Where(x => x.ToLower().Contains(text.ToLower()))
          .OrderBy(x => x)
          .ToList();

        #endregion

        #region METODOS PRIVADOS
        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }

        #endregion

        /*  --------------------- COMAND  ------------------  */

        #region CancelaClikedCommand

        private ICommand _CancelaClikedCommand;
        public ICommand CancelaClikedCommand
        {
            get
            {
                if (_CancelaClikedCommand == null)
                {
                    _CancelaClikedCommand = new RelayCommand(CancelaClikedExecute, CanSubmitExecute);
                }
                return _CancelaClikedCommand;
            }
        }

        public void CancelaClikedExecute(object parameter)
        {
            ShowRecargaMovil = false;
            ShowRecargaNauta = false;
            ShowTipoRecarga = true;
            ShowButton = false;
            ResetFormsMovil();
            RadioMovilCheked = false;
            RadioNautaCheked = false;
        }

        #endregion

        #region AgregarClikedCommand

        private ICommand _AgregarClikedCommand;
        public ICommand AgregarClikedCommand
        {
            get
            {
                if (_AgregarClikedCommand == null)
                {
                    _AgregarClikedCommand = new RelayCommand(AgregarClikedExecute, CanSubmitExecute);
                }
                return _AgregarClikedCommand;
            }
        }

        public void AgregarClikedExecute(object parameter)
        {
            Recargas.Add(new SQ_Recarga { numero = "55043317" });
        }

        #endregion

        #region RadioRecargaMovilCommand

        private ICommand _RadioRecargaMovilCommand;
        public ICommand RadioRecargaMovilCommand
        {
            get
            {
                if (_RadioRecargaMovilCommand == null)
                {
                    _RadioRecargaMovilCommand = new RelayCommand(RadioRecargaMovilExecute, CanSubmitExecute);
                }
                return _RadioRecargaMovilCommand;
            }
        }

        public void RadioRecargaMovilExecute(object parameter)
        {
            ShowRecargaMovil = true;
            ShowRecargaNauta = false;
            ShowTipoRecarga = false;
            ShowButton = true;

        }

        #endregion

        #region RadioRecargaNautaCommand

        private ICommand _RadioRecargaNautaCommand;
        public ICommand RadioRecargaNautaCommand
        {
            get
            {
                if (_RadioRecargaNautaCommand == null)
                {
                    _RadioRecargaNautaCommand = new RelayCommand(RadioRecargaNautaExecute, CanSubmitExecute);
                }
                return _RadioRecargaNautaCommand;
            }
        }

        public void RadioRecargaNautaExecute(object parameter)
        {
            ShowRecargaMovil = false;
            ShowRecargaNauta = true;
            ShowTipoRecarga = false;
            ShowButton = true;
        }

        #endregion

        #region PaisSelectedIndexChangedCommand

        private ICommand _PaisSelectedIndexChangedCommand;
        public ICommand PaisSelectedIndexChangedCommand
        {
            get
            {
                if (_PaisSelectedIndexChangedCommand == null)
                {
                    _PaisSelectedIndexChangedCommand = new RelayCommand(PaisSelectedIndexChangedExecute, CanSubmitExecute);
                }
                return _PaisSelectedIndexChangedCommand;
            }
        }

        public void PaisSelectedIndexChangedExecute(object parameter)
        {
            CargarFotoPais(txtPais);
            txtNumero = Prefijo;
            txtNumeroFocus = true;
        }

        #endregion

        #region ProductoTappedCommand

        private ICommand _ProductoTappedCommand;

        public ICommand ProductoTappedCommand
        {
            get
            {
                if (_ProductoTappedCommand == null)
                {
                    _ProductoTappedCommand = new RelayCommand(ProductoTappedExecute, CanSubmitExecute);
                }
                return _ProductoTappedCommand;
            }
        }

        public void ProductoTappedExecute(object parameter)
        {
            ;
        }

        #endregion

    }
}
