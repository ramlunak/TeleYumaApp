

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeleYumaApp.Class;
using Xamarin.Forms;
using static TeleYumaApp.App;

namespace TeleYumaApp.Contactos
{
    public partial class Llamar : ContentPage
    {
        public Llamar()
        {
            InitializeComponent();
        }

       
        public void LlenarTxtTelefono(string numero)
        {
            txtTelefono.Text = numero;
        }

        void Seleccionar_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                _Global.Vistas.ListaContactos.LlenarLista();
                _Global.Vistas.ListaContactos.Transaction = TipoTransaction.Llamar;
                this.Navigation.PushAsync(_Global.Vistas.ListaContactos);
                _Global.Vistas.ListaContactos.txtLlamar(ref txtTelefono);
            }
            catch
            {

                ;
            }

        }

        private async void BtnLlamar_Clicked(object sender, EventArgs e)
        {
            if (txtTelefono.Text == "" || txtTelefono.Text == null)
            {
                DisplayAlert("TeleYuma", "Escriba el número", "Ok");
                return;
            }

            try
            {
                var numero = txtTelefono.Text;
                var llamada = "7868717144,011" + numero + "#";
                DependencyService.Get<ICallService>().Call(llamada);
              
            }
            catch (Exception ex)
            {

                ;
            }
        }
    }
}
