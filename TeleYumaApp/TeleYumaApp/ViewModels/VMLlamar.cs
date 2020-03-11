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

        ImageCircle ImagePerfil { get; set; }

        EPerfil CurrentPerfil { get; set; }
        //CarouselViewControl Carousel { get; set; }
        Image ImgPromo { get; set; }

        public int ItemsCount = 1;

        public VMLlamar()
        {
           
           
            //CargarContactos();
          
        }

        public async void CargarContactos()
        {
            try
            {
                _Global.ListaContactos = await EContacto.GetListaContactos();
                _Global.Vistas.ListaContactos.LlenarLista();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static bool StarAnimation { get; set; }

        private string _PromoActiva;

        public string PromoActiva
        {
            get { return _PromoActiva; }
            set { _PromoActiva = value; }
        }


        public async Task StartCarousel()
        {
            //try
            //{
            //    if (StarAnimation)
            //    {
            //        var Position = (Carousel.Position + 1) % ItemsCount;

            //        Carousel.Position = Position;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ;
            //}
           
            CargarPromociones();
            await Task.Delay(5000);
            StartCarousel();
        }

        public async Task CargarPromociones()
        {
            var promos = await _Global.Promociones.GetPromos();

            try
            {
                foreach (var item in promos)
                {
                    Stream newstream = new MemoryStream(item.image);
                    ImgPromo.Source = ImageSource.FromStream(() => { return newstream; });
                    await Task.Delay(5000);
                }
                               
            }
            catch (Exception)
            {
                throw;
            }

            //ItemsCount = promos.AsCollectionView().Count;
            // MyItemsSource = promos.AsCollectionView();
            await Task.Delay(5000);
            CargarPromociones();

        }

        public async Task CargarPerfil()
        {
            var login = _Global.SQLiteLogin.GetInfoLogin();
            if (login.foto is null)
            {
                FotoPerfil = "perfil";
                return;
            }

            Stream newstream = new MemoryStream(login.foto);
            ImagePerfil.Source = ImageSource.FromStream(() => { return newstream; });
        }

        ObservableCollection<View> _myItemsSource;
        public ObservableCollection<View> MyItemsSource
        {
            set
            {
                _myItemsSource = value;
                OnPropertyChanged();
            }
            get
            {
                return _myItemsSource;
            }
        }

        public Command MyCommand { protected set; get; }

        public async Task ActualizarInformacionCuenta()
        {
            try
            {
                await _Global.CurrentAccount.GetAccountInfo();
                ActualizarDatos();
                await Task.Delay(3000);
                await ActualizarInformacionCuenta();
            }
            catch
            {
                ;
            }
        }

        public void ActualizarDatos()
        {
            if (_Global.CurrentAccount is null) return;
            saldo = _Global.CurrentAccount.balance.ToString();
            fullname = _Global.CurrentAccount.fullname;
            email = _Global.CurrentAccount.email;
        }

        private string _saldo;
        private string _fullname;
        private string _email;

        public string saldo
        {
            get
            {
                var s = "Saldo: $" + String.Format("{0:#,##0.00}", _Global.CurrentAccount.balance) + " USD";
                return s;
            }
            set
            {
                _saldo = value;
                OnPropertyChanged();
            }
        }

        public string fullname
        {
            get
            {
                return _fullname;
            }
            set
            {
                _fullname = value;
                OnPropertyChanged();
            }

        }

        public string email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }


        private string _FotoPerfil;

        public string FotoPerfil
        {
            get { return _FotoPerfil; }
            set { _FotoPerfil = value; OnPropertyChanged(); }
        }


        private bool CanSubmitExecute(object parameter)
        {
            return true;
        }


        private ICommand _FotoPerfilTappedCommand;
        public ICommand FotoPerfilTappedCommand
        {
            get
            {
                if (_FotoPerfilTappedCommand == null)
                {
                    _FotoPerfilTappedCommand = new RelayCommand(FotoPerfilTappedExecute, CanSubmitExecute);
                }
                return _FotoPerfilTappedCommand;
            }
        }

        public async void FotoPerfilTappedExecute(object parameter)
        {

            var result = await CurrentPage.DisplayAlert("Teleyuma.", "Desea actualizar la foto de perfil?.", "Si", "No");
            if (!result) return;

            Stream stream = await DependencyService.Get<IPicturePicker>().GetImageStreamAsync();
            if (stream != null)
            {
                byte[] img = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    stream.CopyTo(ms);
                    img = ms.ToArray();
                }

                //var p = new EPerfil
                //{
                //    IdCuenta = _Global.CurrentAccount.phone1,
                //    Foto = img
                //};

                //var promo = new EPromo
                //{

                //    image = img
                //};

                //var fg = _Global.Post<EPromo>("Promo", promo);              
                //StarAnimation = false;
                //await  CargarPromociones();
                //EPerfil perfil;

                _Global.SQLiteLogin.foto = img;
                if (_Global.SQLiteLogin.SetPhotoPerfil())
                {
                    Stream newstream = new MemoryStream(img);
                    ImagePerfil.Source = ImageSource.FromStream(() => { return newstream; });
                }
                else
                {
                    CurrentPage.DisplayAlert("Teleyuma.", "No se pudo actualizar su foto de perfil.", "Ok");
                    return;
                }

                //try
                //{
                //   // perfil = await _Global.CurrentAccount.PostPerfil(p);
                //    //perfil = await _Global.Post<EPerfil>("Perfil", p);
                //}
                //catch (Exception ex)
                //{
                //    CurrentPage.DisplayAlert("Teleyuma.", "No se pudo actualizar su foto de perfil.", "Ok");
                //    return;
                //}

                //if (perfil.Foto is null)
                //{
                //    CurrentPage.DisplayAlert("Teleyuma.", "No se pudo actualizar su foto de perfil.", "Ok");
                //    return;
                //}

                //if (perfil != null)
                //{
                //    CurrentPerfil = perfil;
                //    Stream newstream = new MemoryStream(CurrentPerfil.Foto);
                //    ImagePerfil.Source = ImageSource.FromStream(() => { return newstream; });
                //}

            }
        }

    }
}
