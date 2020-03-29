using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TeleYumaApp.Class;
using TeleYumaApp.Teleyuma;
using Xamarin.Forms;


namespace TeleYumaApp
{
    public partial class App : Application
    {
        public interface ICloseApplication
        {
            void closeApplication();
        }

        public interface ICallService
        {
            void Call(string number);
        }

        public App()
        {
            InitializeComponent();
          
            Login();
        }


        public async void Login()
        {
         
            try
            {

                _Global.phone.CargarContactos();
                _Global.VM.VMGrupos.CargarSMS();

                var login = _Global.SQLiteLogin.GetInfoLogin();
                if (login != null)
                {
                    _Global.CurrentAccount.i_account = login.i_account;


                    _Global.CurrentAccount.GetAccountInfo();
                    _Global.VM.VMInicio.ActualizarInformacionCuenta();
                    _Global.VM.VMInicio.ActualizarDatos();
                   
                    MainPage = new Pages.MasterDetail();

                    if (_Global.IdSmsFromNoti != 0)
                    {


                    }
                }
                else
                {
                    MainPage = new NavigationPage(new PagesInicio.Login());
                }
            }
            catch
            {
                try
                {
                    MainPage = new NavigationPage(new PagesInicio.Login());
                }
                catch (Exception ex)
                {

                    App.Current.MainPage.DisplayAlert("System", ex.Message, "ok");
                }
            }

        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            _Global.IsOpen = false;
            //  Application.Current.Properties["MainPage"] = Application.Current.MainPage;
            ;
        }

        protected override void OnResume()
        {
            // Application.Current.MainPage = (Page)Application.Current.Properties["MainPage"];
        }

    }

}
